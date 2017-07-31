using Reflector;
using Reflector.CodeModel;
using Reflector.CodeModel.Memory;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace FileGenerator.AddIn.Generators
{
	internal sealed class TypeFileGenerator : FileGenerator<ITypeDeclaration>
	{
		public override event FileCreatedEventHandler FileCreated;

		private string fullTypeName = string.Empty;

		internal TypeFileGenerator(FileGeneratorContext<ITypeDeclaration> context)
			: base(context)
		{
			this.TypeCount = 1;
			this.fullTypeName = this.Context.Item.Namespace + "." + this.Context.Item.Name;
		}

		private static string RemoveInvalidCharacters(string data)
		{
			// TODO - 1/14/2006 - This feels awkward - is there a better way to 
			// cleanse a file path with bad characters?
			return data.Replace('/', '_').Replace('\\', '_').Replace(':', '_')
				 .Replace('*', '_').Replace('?', '_').Replace('"', '_')
				 .Replace('<', '_').Replace('>', '_').Replace('|', '_');
		}

		private void CreateFile(IFormatter formatter)
		{
			// TODO - 1/17/2006 - If the file exists...do we overwrite it, or 
			// create a new file? The issue deals with obfuscation and type names
			// being the same, yet...is that an old file, or one created before with the
			// same name? Maybe type handles could be used...

			var fileName = TypeFileGenerator.RemoveInvalidCharacters(
				this.fullTypeName);

			if(fileName.StartsWith(".", StringComparison.CurrentCultureIgnoreCase))
			{
				fileName = fileName.Substring(1);
			}

			if(this.Context.CreateSubdirectories)
			{
				fileName = fileName.Replace('.', '\\');
			}

			fileName = Path.Combine(this.Context.Directory,
				fileName + this.Context.Language.FileExtension);

			var filePath = Path.GetDirectoryName(fileName);

			if(Directory.Exists(filePath) == false)
			{
				Directory.CreateDirectory(filePath);
			}

			using(var typeFile = new StreamWriter(fileName))
			{
				typeFile.Write(formatter.ToString());

				if(this.Context.CreateVsNetProject)
				{
					base.AddGeneratedFileToCompileElement(fileName);
				}

				if(this.FileCreated != null)
				{
					this.FileCreated(this, new FileGeneratedEventArgs(fileName));
				}
			}
		}

		private IFormatter GetFormatter()
		{
			var formatter = new TextFormatter();
			var languageWriterConfiguration = new LanguageWriterConfiguration();
			var writer = this.Context.Language.GetWriter(
				formatter, languageWriterConfiguration);

			this.Context.Item = this.Context.Translator.TranslateTypeDeclaration(
				this.Context.Item, true, true);

			// NOTE - 1/18/2006 - This is done to ensure all of the
			// type information is written, esp. if namespace information is present.
			if(this.Context.Item.Namespace != null && this.Context.Item.Namespace.Length > 0)
			{
				var typeNamespace = new Namespace();
				typeNamespace.Name = this.Context.Item.Namespace;

				typeNamespace.Types.Add(this.Context.Item);
				writer.WriteNamespace(typeNamespace);
			}
			else
			{
				writer.WriteTypeDeclaration(this.Context.Item);
			}

			return formatter;
		}

		public override void Generate()
		{
			var assembly = ((IModule)this.Context.Item.Owner).Assembly;

			base.InitializeProject(assembly);

			var formatter = this.GetFormatter();
			this.CreateFile(formatter);

			base.SaveProject(this.Context.Item.Name);
		}
	}
}
