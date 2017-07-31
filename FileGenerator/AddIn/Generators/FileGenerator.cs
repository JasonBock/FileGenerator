using Microsoft.Build.BuildEngine;
using Reflector;
using Reflector.CodeModel;
using Spackle.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace FileGenerator.AddIn.Generators
{
	internal abstract class FileGenerator<T> : IFileGenerator
	{
		public abstract event FileCreatedEventHandler FileCreated;

		protected FileGenerator(FileGeneratorContext<T> context)
			: base()
		{
			context.CheckParameterForNull("context");
			this.Context = context;
			this.SubDirectories = new List<string>();
		}

		protected void AddGeneratedFileToCompileElement(string filePath)
		{
			if(this.Context.IsRoot && this.Context.CreateVsNetProject)
			{
				var codeFileName = Path.GetFileName(filePath);
				var codeDirectory = Path.GetDirectoryName(filePath) ?? string.Empty;
				codeDirectory = codeDirectory.Replace(
					this.Context.Directory, string.Empty);

				if(codeDirectory.StartsWith(@"\", StringComparison.CurrentCultureIgnoreCase))
				{
					codeDirectory = codeDirectory.Substring(1);
				}

				if(codeDirectory.Length > 0)
				{
					codeDirectory += @"\";
				}

				if(this.Context.CreateSubdirectories)
				{
					if(!this.SubDirectories.Contains(codeDirectory))
					{
						this.SubDirectories.Add(codeDirectory);
						this.CompileFiles.AddNewItem("Compile", codeDirectory + "*" +
							this.Context.Language.FileExtension);
					}
				}
				else
				{
					this.CompileFiles.AddNewItem("Compile", codeDirectory + codeFileName);
				}
			}
		}

		public abstract void Generate();

		private void CreateImportTargets()
		{
			var importProjectFile = @"$(MSBuildBinPath)\";

			if(this.Context.Language.Name == "C#")
			{
				importProjectFile += "Microsoft.CSharp.targets";
			}
			else if(this.Context.Language.Name == "Visual Basic")
			{
				importProjectFile += "Microsoft.VisualBasic.targets";
			}
			else
			{
				importProjectFile += "Microsoft.Common.targets";
			}

			this.Project.AddNewImport(importProjectFile, string.Empty);
		}

		private void CreatePropertyGroups()
		{
			var debugGroup = this.Project.AddNewPropertyGroup(false);
			debugGroup.Condition = " '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' ";
			debugGroup.AddNewProperty("DebugSymbols", "true");
			debugGroup.AddNewProperty("DebugType", "full");
			debugGroup.AddNewProperty("OutputPath", @"bin\Debug\");
			debugGroup.AddNewProperty("Optimize", "false");

			var releaseGroup = this.Project.AddNewPropertyGroup(false);
			releaseGroup.Condition = " '$(Configuration)|$(Platform)' == 'Release|AnyCPU' ";
			releaseGroup.AddNewProperty("DebugSymbols", "false");
			releaseGroup.AddNewProperty("DebugType", "pdbonly");
			releaseGroup.AddNewProperty("OutputPath", @"bin\Release\");
			releaseGroup.AddNewProperty("Optimize", "true");
		}

		private void InitializeAssemblyReferencesGroup(IAssembly baseAssembly)
		{
			if(baseAssembly.AssemblyManager.Assemblies.Count > 0)
			{
				var referencedAssemblies = new List<IAssembly>();

				foreach(IAssembly referencedAssembly in
					baseAssembly.AssemblyManager.Assemblies)
				{
					if(referencedAssembly != baseAssembly)
					{
						referencedAssemblies.Add(referencedAssembly);
					}
				}

				if(referencedAssemblies.Count > 0)
				{
					var references = this.Project.AddNewItemGroup();

					foreach(var referencedAssembly in referencedAssemblies)
					{
						references.AddNewItem("Reference", referencedAssembly.Name);
					}
				}
			}
		}

		private void InitializeCompileGroup()
		{
			if(this.TypeCount > 0)
			{
				this.CompileFiles = this.Project.AddNewItemGroup();
			}
		}

		protected void InitializeProject(IAssembly baseAssembly)
		{
			if(this.Context.IsRoot && this.Context.CreateVsNetProject && this.Context.CreateVsNetProject)
			{
				this.Project = new Project(new Engine());
				this.Project.DefaultTargets = "Build";
				this.CreatePropertyGroups();
				this.InitializeAssemblyReferencesGroup(baseAssembly);
				this.InitializeCompileGroup();
				this.CreateImportTargets();

				if(this.Context.CreateSubdirectories)
				{
					this.SubDirectories = new List<string>();
				}
			}
		}

		protected void SaveProject(string projectName)
		{
			if(this.Context.IsRoot && this.Context.CreateVsNetProject && this.Project != null)
			{
				var projectExtension = string.Empty;

				if(this.Context.Language.Name == "C#")
				{
					projectExtension = ".csproj";
				}
				else if(this.Context.Language.Name == "Visual Basic")
				{
					projectExtension += ".vbproj";
				}
				else
				{
					projectExtension += ".msbuild";
				}

				string projectFileName = projectName + projectExtension;
				this.Project.Save(Path.Combine(this.Context.Directory, projectFileName));
			}
		}

		internal BuildItemGroup CompileFiles
		{
			get;
			private set;
		}

		internal FileGeneratorContext<T> Context
		{
			get;
			private set;
		}

		internal Project Project
		{
			get;
			private set;
		}

		internal List<string> SubDirectories
		{
			get;
			private set;
		}

		public int TypeCount
		{
			get;
			protected set;
		}
	}
}
