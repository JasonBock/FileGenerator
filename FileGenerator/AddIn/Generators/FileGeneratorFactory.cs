using Reflector;
using Reflector.CodeModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace FileGenerator.AddIn.Generators
{
	internal static class FileGeneratorFactory
	{
		internal const int EventWaitTime = 1;

		internal static IFileGenerator Create<TItem>(TItem activeItem, string directory,
			ITranslator visitor, ILanguage language, ManualResetEvent cancel,
			bool createSubdirectories, bool createVsNetProject)
		{
			IFileGenerator generator = null;

			var activeItemType = activeItem.GetType();
			
			if(typeof(IAssembly).IsAssignableFrom(activeItemType))
			{
				generator = FileGeneratorFactory.Create<IAssembly>(
					activeItem as IAssembly, directory, visitor, language, cancel, createSubdirectories, createVsNetProject,
					(context) =>
					{
						return new AssemblyFileGenerator(context);
					});
			}
			else if(typeof(IModule).IsAssignableFrom(activeItemType))
			{
				generator = FileGeneratorFactory.Create<IModule>(
					activeItem as IModule, directory, visitor, language, cancel, createSubdirectories, createVsNetProject,
					(context) =>
					{
						return new ModuleFileGenerator(context);
					});
			}
			else if(typeof(INamespace).IsAssignableFrom(activeItemType))
			{
				generator = FileGeneratorFactory.Create<INamespace>(
					activeItem as INamespace, directory, visitor, language, cancel, createSubdirectories, createVsNetProject,
					(context) =>
					{
						return new NamespaceFileGenerator(context);
					});
			}
			else if(typeof(ITypeDeclaration).IsAssignableFrom(activeItemType))
			{
				generator = FileGeneratorFactory.Create<ITypeDeclaration>(
					activeItem as ITypeDeclaration, directory, visitor, language, cancel, createSubdirectories, createVsNetProject,
					(context) =>
					{
						return new TypeFileGenerator(context);
					});
			}

			return generator;
		}

		private static IFileGenerator Create<TItem>(
			TItem activeItem, string directory, ITranslator visitor,
			ILanguage language, ManualResetEvent cancel,
			bool createSubdirectories, bool createVsNetProject,
			Func<FileGeneratorContext<TItem>, IFileGenerator> generatorCreator)
			where TItem : class
		{
			return generatorCreator(new FileGeneratorContext<TItem>(
				activeItem, directory, language, visitor, cancel,
				createSubdirectories, createVsNetProject, true));
		}
	}
}
