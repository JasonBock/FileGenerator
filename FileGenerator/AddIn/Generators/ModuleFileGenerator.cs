using Reflector;
using Reflector.CodeModel;
using System;
using System.Collections.Generic;
using System.Threading;

namespace FileGenerator.AddIn.Generators
{
	internal sealed class ModuleFileGenerator : FileGenerator<IModule>
	{
		public override event FileCreatedEventHandler FileCreated;

		internal ModuleFileGenerator(FileGeneratorContext<IModule> context)
			: base(context)
		{
			this.TypeCount = this.Context.Item.Types.Count;
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
			base.InitializeProject(this.Context.Item.Assembly);

			foreach(ITypeDeclaration typeDeclaration in this.Context.Item.Types)
			{
				var context = new FileGeneratorContext<ITypeDeclaration>(
						typeDeclaration, this.Context.Directory, this.Context.Language,
						this.Context.Translator, this.Context.Cancel,
						this.Context.CreateSubdirectories, this.Context.CreateVsNetProject);
				var typeGenerator = new TypeFileGenerator(context);
				var fileCreatedHandler = new FileCreatedEventHandler(this.OnFileGenerated);

				try
				{
					typeGenerator.FileCreated += fileCreatedHandler;
					typeGenerator.Generate();
				}
				finally
				{
					typeGenerator.FileCreated -= fileCreatedHandler;
				}

				if(this.Context.Cancel.WaitOne(FileGeneratorFactory.EventWaitTime, false) == true)
				{
					break;
				}
			}

			base.SaveProject(this.Context.Item.Name);
		}
	}
}
