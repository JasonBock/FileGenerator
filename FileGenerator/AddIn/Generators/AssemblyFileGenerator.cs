using Microsoft.Build.BuildEngine;
using Reflector;
using Reflector.CodeModel;
using Reflector.CodeModel.Memory;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;

namespace FileGenerator.AddIn.Generators
{
	internal sealed class AssemblyFileGenerator : FileGenerator<IAssembly>
	{
		public override event FileCreatedEventHandler FileCreated;

		internal AssemblyFileGenerator(FileGeneratorContext<IAssembly> context)
			: base(context)
		{
			foreach(IModule module in this.Context.Item.Modules)
			{
				this.TypeCount += module.Types.Count;
			}
		}

		private static void AddResourceToBuild(IResource resource, string resourceFileName,
			BuildItemGroup resources)
		{
			var resourceItem = resources.AddNewItem(
				"EmbeddedResource", resourceFileName);

			if(resource.Visibility == ResourceVisibility.Private)
			{
				resourceItem.SetMetadata("Visible", "false");
			}
		}

		private void GenerateAssemblyAttributesFile()
		{
			if(this.Context.Item.Attributes.Count > 0)
			{
				var formatter = new TextFormatter();
				var languageWriterConfiguration =
						  new LanguageWriterConfiguration();
				var writer = this.Context.Language.GetWriter(
						  formatter, languageWriterConfiguration);
				writer.WriteAssembly(this.Context.Item);

				string attributesFileName = Path.Combine(
					this.Context.Directory, "GeneratedAssemblyInfo" +
					this.Context.Language.FileExtension);

				using(var attributesFile = new StreamWriter(attributesFileName))
				{
					attributesFile.Write(formatter.ToString());
					this.AddGeneratedFileToCompileElement(attributesFileName);

					if(this.FileCreated != null)
					{
						this.FileCreated(this, new FileGeneratedEventArgs(attributesFileName));
					}
				}
			}
		}

		private void InitializeEmbeddedResourcesGroup()
		{
			if(this.Context.Item.Resources != null && this.Context.Item.Resources.Count > 0)
			{
				var embeddedResources = this.Project.AddNewItemGroup();

				foreach(var resource in this.Context.Item.Resources)
				{
					var fileRes = resource as FileResource;

					if(fileRes != null)
					{
						this.SaveFileResource(fileRes, embeddedResources);
					}
					else
					{
						var embedRes = resource as EmbeddedResource;

						if(embedRes != null)
						{
							this.SaveEmbeddedResource(embedRes, embeddedResources);
						}
					}
				}
			}
		}

		private void OnFileGenerated(object sender, FileGeneratedEventArgs e)
		{
			base.AddGeneratedFileToCompileElement(e.FileName);

			if(this.FileCreated != null)
			{
				this.FileCreated(this, e);
			}
		}

		public override void Generate()
		{
			base.InitializeProject(this.Context.Item);
			this.InitializeEmbeddedResourcesGroup();
			this.GenerateAssemblyAttributesFile();

			foreach(IModule module in this.Context.Item.Modules)
			{
				var context = new FileGeneratorContext<IModule>(module,
					this.Context.Directory, this.Context.Language,
					this.Context.Translator, this.Context.Cancel,
					this.Context.CreateSubdirectories, this.Context.CreateVsNetProject);

				var moduleGenerator = new ModuleFileGenerator(context);

				var fileCreatedHandler = new FileCreatedEventHandler(this.OnFileGenerated);

				try
				{
					moduleGenerator.FileCreated += fileCreatedHandler;
					moduleGenerator.Generate();
				}
				finally
				{
					moduleGenerator.FileCreated -= fileCreatedHandler;
				}

				if(this.Context.Cancel.WaitOne(FileGeneratorFactory.EventWaitTime, false) == true)
				{
					break;
				}
			}

			base.SaveProject(this.Context.Item.Name);
		}

		private void SaveEmbeddedResource(EmbeddedResource resource, BuildItemGroup resources)
		{
			string resourceFileName = Path.Combine(
				this.Context.Directory, resource.Name);

			using(var resourceFile = new BinaryWriter(
				File.Create(resourceFileName)))
			{
				resourceFile.Write(resource.Value);
				AssemblyFileGenerator.AddResourceToBuild(resource, resourceFileName, resources);

				if(this.FileCreated != null)
				{
					this.FileCreated(this, new FileGeneratedEventArgs(resourceFileName));
				}
			}
		}

		private void SaveFileResource(FileResource resource, BuildItemGroup resources)
		{
			var formatter = new TextFormatter();
			var languageWriterConfiguration =
					  new LanguageWriterConfiguration();
			var writer = this.Context.Language.GetWriter(
					  formatter, languageWriterConfiguration);
			writer.WriteResource(resource);

			string resourceFileName = Path.Combine(
				this.Context.Directory, resource.Name);

			using(var resourceFile = new StreamWriter(resourceFileName))
			{
				resourceFile.Write(formatter.ToString());
				AssemblyFileGenerator.AddResourceToBuild(resource, resourceFileName, resources);

				if(this.FileCreated != null)
				{
					this.FileCreated(this, new FileGeneratedEventArgs(resourceFileName));
				}
			}
		}
	}
}
