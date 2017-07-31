using Reflector;
using Reflector.CodeModel;
using Spackle.Extensions;
using System;
using SI = System.IO;
using System.Threading;

namespace FileGenerator.AddIn.Generators
{
	internal sealed class FileGeneratorContext<T>
	{
		internal FileGeneratorContext(T item, string directory, ILanguage language, ITranslator translator, ManualResetEvent cancel, bool createSubdirectories, bool createVsNetProject)
			: this(item, directory, language, translator, cancel, createSubdirectories, createVsNetProject, false)
		{
		}

		internal FileGeneratorContext(T item, string directory, ILanguage language, ITranslator translator, ManualResetEvent cancel, bool createSubdirectories, bool createVsNetProject, bool isRoot)
		{
			directory.CheckParameterForNull("directory");
			translator.CheckParameterForNull("translator");
			language.CheckParameterForNull("language");
			cancel.CheckParameterForNull("cancel");
			item.CheckParameterForNull("item");
			
			if(!SI.Directory.Exists(directory))
			{
				throw new SI.DirectoryNotFoundException(directory + " cannot be found.");
			}

			this.IsRoot = isRoot;
			this.Item = item;
			this.CreateSubdirectories = createSubdirectories;
			this.CreateVsNetProject = createVsNetProject;

			var separator = new string(SI.Path.DirectorySeparatorChar, 1);

			if(directory.EndsWith(separator, StringComparison.CurrentCultureIgnoreCase))
			{
				directory = directory.Substring(0, directory.Length - 1);
			}

			this.Directory = directory;
			this.Translator = translator;
			this.Language = language;
			this.Cancel = cancel;
		}

		internal ManualResetEvent Cancel
		{
			get;
			private set;
		}

		internal bool CreateSubdirectories
		{
			get;
			private set;
		}

		internal bool CreateVsNetProject
		{
			get;
			private set;
		}

		internal string Directory
		{
			get;
			private set;
		}

		internal bool IsRoot
		{
			get;
			private set;
		}

		internal T Item
		{
			get;
			set;
		}

		internal ILanguage Language
		{
			get;
			private set;
		}

		internal ITranslator Translator
		{
			get;
			private set;
		}
	}
}
