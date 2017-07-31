using Reflector;
using Reflector.CodeModel;
using System;
using System.Collections.Generic;
using System.Threading;

namespace FileGenerator.AddIn.Generators
{
	internal sealed class NamespaceFileGenerator : FileGenerator<INamespace>
	{
		public override event FileCreatedEventHandler FileCreated;

		internal NamespaceFileGenerator(FileGeneratorContext<INamespace> context)
			: base(context)
		{
			base.TypeCount = this.Context.Item.Types.Count;
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
			if(this.Context.Item.Types != null && this.Context.Item.Types.Count > 0)
			{
				var assembly = ((IModule)this.Context.Item.Types[0].Owner).Assembly;
				base.InitializeProject(assembly);

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
}
