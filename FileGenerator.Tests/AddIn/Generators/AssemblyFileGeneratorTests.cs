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
	public sealed class AssemblyFileGeneratorTests : CoreTests
	{
		private void Create(string languageName, string projectExtension, string fileExtension)
		{
			var item = AssemblyTests.GetAssembly("FileGenerator.Tests.Target");
			var outputPath = this.TestContext.GetOutputPath(this.GetType().Name);
			Directory.CreateDirectory(outputPath);

			using(var @event = new ManualResetEvent(false))
			{
				FileGeneratorFactory.Create(
					item, outputPath,
					AssemblyTests.GetTranslator(),
					AssemblyTests.GetLanguage(languageName),
					@event, true, true).Generate();

				Assert.IsTrue(File.Exists(Path.Combine(outputPath, "FileGenerator.Tests.Target" + projectExtension)));
				Assert.IsTrue(File.Exists(Path.Combine(outputPath, "_Module_" + fileExtension)));
				Assert.IsTrue(File.Exists(Path.Combine(outputPath, "GeneratedAssemblyInfo" + fileExtension)));

				var baseContentPath = Path.Combine(outputPath, @"FileGenerator\Tests\Target");
				Assert.IsTrue(Directory.Exists(baseContentPath));
				Assert.IsTrue(File.Exists(Path.Combine(baseContentPath, "InBaseNamespace" + fileExtension)));

				var oneContentPath = Path.Combine(baseContentPath, "One");
				Assert.IsTrue(Directory.Exists(oneContentPath));
				Assert.IsTrue(File.Exists(Path.Combine(oneContentPath, "InOneNamespace" + fileExtension)));

				var twoContentPath = Path.Combine(baseContentPath, "Two");
				Assert.IsTrue(Directory.Exists(twoContentPath));
				Assert.IsTrue(File.Exists(Path.Combine(twoContentPath, "InTwoNamespace" + fileExtension)));
			}
		}

		[TestMethod]
		public void CreateViaCSharp()
		{
			this.Create(LanguageInformation.CSharp, LanguageInformation.CSharpProjectExtension,
				LanguageInformation.CSharpFileExtension);
		}

		[TestMethod]
		public void CreateViaVB()
		{
			this.Create(LanguageInformation.VB, LanguageInformation.VBProjectExtension,
				LanguageInformation.VBFileExtension);
		}

		[TestMethod]
		public void CreateViaIL()
		{
			this.Create(LanguageInformation.IL, LanguageInformation.ILProjectExtension,
				LanguageInformation.ILFileExtension);
		}

		[TestMethod]
		public void CreateWithEventListener()
		{
			var item = AssemblyTests.GetAssembly("FileGenerator.Tests.Target");
			var outputPath = this.TestContext.GetOutputPath(this.GetType().Name);
			Directory.CreateDirectory(outputPath);

			using(var @event = new ManualResetEvent(false))
			{
				var generator = FileGeneratorFactory.Create(
					item, outputPath,
					AssemblyTests.GetTranslator(),
					AssemblyTests.GetLanguage(LanguageInformation.CSharp),
					@event, true, true);

				int count = 0;
				var fileGeneratedListener = new FileCreatedEventHandler((sender, e) =>
				{
					count++;
				});

				generator.FileCreated += fileGeneratedListener;
				generator.Generate();
				generator.FileCreated -= fileGeneratedListener;

				Assert.AreEqual(5, count);
			}
		}

		private void CreateWithNoSubdirectories(string languageName, string projectExtension, string fileExtension)
		{
			var item = AssemblyTests.GetAssembly("FileGenerator.Tests.Target");
			var outputPath = this.TestContext.GetOutputPath(this.GetType().Name);
			Directory.CreateDirectory(outputPath);

			using(var @event = new ManualResetEvent(false))
			{
				FileGeneratorFactory.Create(
					item, outputPath,
					AssemblyTests.GetTranslator(),
					AssemblyTests.GetLanguage(languageName),
					@event, false, true).Generate();

				Assert.IsTrue(File.Exists(Path.Combine(outputPath, "FileGenerator.Tests.Target" + projectExtension)));
				Assert.IsTrue(File.Exists(Path.Combine(outputPath, "_Module_" + fileExtension)));
				Assert.IsTrue(File.Exists(Path.Combine(outputPath, "GeneratedAssemblyInfo" + fileExtension)));
				Assert.IsTrue(File.Exists(Path.Combine(outputPath, "FileGenerator.Tests.Target.InBaseNamespace" + fileExtension)));
				Assert.IsTrue(File.Exists(Path.Combine(outputPath, "FileGenerator.Tests.Target.One.InOneNamespace" + fileExtension)));
				Assert.IsTrue(File.Exists(Path.Combine(outputPath, "FileGenerator.Tests.Target.Two.InTwoNamespace" + fileExtension)));
			}
		}

		[TestMethod]
		public void CreateWithNoSubdirectoriesViaCSharp()
		{
			this.CreateWithNoSubdirectories(LanguageInformation.CSharp,
				LanguageInformation.CSharpProjectExtension, LanguageInformation.CSharpFileExtension);
		}

		[TestMethod]
		public void CreateWithNoSubdirectoriesViaVB()
		{
			this.CreateWithNoSubdirectories(LanguageInformation.VB,
				LanguageInformation.VBProjectExtension, LanguageInformation.VBFileExtension);
		}

		[TestMethod]
		public void CreateWithNoSubdirectoriesViaIL()
		{
			this.CreateWithNoSubdirectories(LanguageInformation.IL,
				LanguageInformation.ILProjectExtension, LanguageInformation.ILFileExtension);
		}

		private void CreateWithNoProjectFile(string languageName, string projectExtension, string fileExtension)
		{
			var item = AssemblyTests.GetAssembly("FileGenerator.Tests.Target");
			var outputPath = this.TestContext.GetOutputPath(this.GetType().Name);
			Directory.CreateDirectory(outputPath);

			using(var @event = new ManualResetEvent(false))
			{
				FileGeneratorFactory.Create(
					item, outputPath,
					AssemblyTests.GetTranslator(),
					AssemblyTests.GetLanguage(languageName),
					@event, true, false).Generate();

				Assert.IsFalse(File.Exists(Path.Combine(outputPath, "FileGenerator.Tests.Target" + projectExtension)));
				Assert.IsTrue(File.Exists(Path.Combine(outputPath, "_Module_" + fileExtension)));
				Assert.IsTrue(File.Exists(Path.Combine(outputPath, "GeneratedAssemblyInfo" + fileExtension)));

				var baseContentPath = Path.Combine(outputPath, @"FileGenerator\Tests\Target");
				Assert.IsTrue(Directory.Exists(baseContentPath));
				Assert.IsTrue(File.Exists(Path.Combine(baseContentPath, "InBaseNamespace" + fileExtension)));

				var oneContentPath = Path.Combine(baseContentPath, "One");
				Assert.IsTrue(Directory.Exists(oneContentPath));
				Assert.IsTrue(File.Exists(Path.Combine(oneContentPath, "InOneNamespace" + fileExtension)));

				var twoContentPath = Path.Combine(baseContentPath, "Two");
				Assert.IsTrue(Directory.Exists(twoContentPath));
				Assert.IsTrue(File.Exists(Path.Combine(twoContentPath, "InTwoNamespace" + fileExtension)));
			}
		}
		
		[TestMethod]
		public void CreateWithNoProjectFileViaCSharp()
		{
			this.CreateWithNoProjectFile(LanguageInformation.CSharp,
				LanguageInformation.CSharpProjectExtension, LanguageInformation.CSharpFileExtension);
		}

		[TestMethod]
		public void CreateWithNoProjectFileViaVB()
		{
			this.CreateWithNoProjectFile(LanguageInformation.VB,
				LanguageInformation.VBProjectExtension, LanguageInformation.VBFileExtension);
		}

		[TestMethod]
		public void CreateWithNoProjectFileViaIL()
		{
			this.CreateWithNoProjectFile(LanguageInformation.IL,
				LanguageInformation.ILProjectExtension, LanguageInformation.ILFileExtension);
		}
	}
}
