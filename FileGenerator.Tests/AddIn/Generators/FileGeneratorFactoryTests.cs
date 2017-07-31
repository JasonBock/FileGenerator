using FileGenerator.AddIn.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reflector;
using Reflector.CodeModel;
using Spackle.Testing;
using System;
using System.IO;
using System.Threading;

namespace FileGenerator.Tests.AddIn.Generators
{
	[TestClass]
	public sealed class FileGeneratorFactoryTests : CoreTests
	{
		[TestMethod]
		public void CreateAssemblyFileGenerator()
		{
			const string Directory = @"C:\Windows";
			var item = new MockAssembly();
			var language = new MockLanguage();
			var translator = new MockTranslator();

			using(var @event = new ManualResetEvent(false))
			{
				var generator = FileGeneratorFactory.Create<IAssembly>(
					item, Directory, translator, language, @event, true, true);
				Assert.AreEqual(item, (generator as FileGenerator<IAssembly>).Context.Item);
			}
		}

		[TestMethod]
		public void CreateModuleFileGenerator()
		{
			const string Directory = @"C:\Windows";
			var item = new MockModule();
			var language = new MockLanguage();
			var translator = new MockTranslator();

			using(var @event = new ManualResetEvent(false))
			{
				var generator = FileGeneratorFactory.Create<IModule>(
					item, Directory, translator, language, @event, true, true);
				Assert.AreEqual(item, (generator as FileGenerator<IModule>).Context.Item);
			}
		}

		[TestMethod]
		public void CreateNamespaceFileGenerator()
		{
			const string Directory = @"C:\Windows";
			var item = new MockNamespace();
			var language = new MockLanguage();
			var translator = new MockTranslator();

			using(var @event = new ManualResetEvent(false))
			{
				var generator = FileGeneratorFactory.Create<INamespace>(
					item, Directory, translator, language, @event, true, true);
				Assert.AreEqual(item, (generator as FileGenerator<INamespace>).Context.Item);
			}
		}

		[TestMethod]
		public void CreateTypeFileGenerator()
		{
			const string Directory = @"C:\Windows";
			var item = new MockTypeDeclaration();
			var language = new MockLanguage();
			var translator = new MockTranslator();

			using(var @event = new ManualResetEvent(false))
			{
				var generator = FileGeneratorFactory.Create<ITypeDeclaration>(
					item, Directory, translator, language, @event, true, true);
				Assert.AreEqual(item, (generator as FileGenerator<ITypeDeclaration>).Context.Item);
			}
		}
	}
}
