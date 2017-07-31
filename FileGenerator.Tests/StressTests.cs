using FileGenerator.AddIn.Generators;
using FileGenerator.Tests.AddIn.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reflector;
using Reflector.CodeModel;
using Spackle.Testing;
using System;
using System.IO;
using System.Threading;

namespace FileGenerator.Tests
{
	[TestClass]
	public sealed class StressTests : CoreTests
	{
		[TestMethod, Timeout(Int32.MaxValue)]
		public void GenerateAllMscorlibMethods()
		{
			var item = AssemblyTests.GetAssembly("mscorlib");
			var outputPath = this.TestContext.GetOutputPath(this.GetType().Name);
			Directory.CreateDirectory(outputPath);

			using(var @event = new ManualResetEvent(false))
			{
				var generator = FileGeneratorFactory.Create(
					item, outputPath,
					AssemblyTests.GetTranslator(),
					AssemblyTests.GetLanguage(LanguageInformation.CSharp),
					@event, true, true);

				Assert.IsTrue(generator.TypeCount > 0);
				
				var generatedFileCount = 0;

				var fileGeneratedHandler = new FileCreatedEventHandler((sender, args) =>
				{
					generatedFileCount++;
				});

				generator.FileCreated += fileGeneratedHandler;

				try
				{
					generator.Generate();
				}
				finally
				{
					generator.FileCreated -= fileGeneratedHandler;
				}
				
				Assert.IsTrue(generatedFileCount >= generator.TypeCount);
			}
		}
	}
}
