using FileGenerator.AddIn;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spackle.Testing;
using System;

namespace FileGenerator.Tests.AddIn
{
	[TestClass]
	public sealed class LanguageWriterConfigurationTests : CoreTests
	{
		[TestMethod]
		public void CheckShowDocumentationIndexer()
		{
			Assert.AreEqual("true", new LanguageWriterConfiguration()["ShowDocumentation"]);
		}

		[TestMethod]
		public void CheckShowCustomAttributesIndexer()
		{
			Assert.AreEqual("true", new LanguageWriterConfiguration()["ShowCustomAttributes"]);
		}

		[TestMethod]
		public void CheckShowNamespaceImportsIndexer()
		{
			Assert.AreEqual("true", new LanguageWriterConfiguration()["ShowNamespaceImports"]);
		}

		[TestMethod]
		public void CheckShowNamespaceBodyIndexer()
		{
			Assert.AreEqual("true", new LanguageWriterConfiguration()["ShowNamespaceBody"]);
		}

		[TestMethod]
		public void CheckShowTypeDeclarationBodyIndexer()
		{
			Assert.AreEqual("true", new LanguageWriterConfiguration()["ShowTypeDeclarationBody"]);
		}

		[TestMethod]
		public void CheckShowMethodDeclarationBodyIndexer()
		{
			Assert.AreEqual("true", new LanguageWriterConfiguration()["ShowMethodDeclarationBody"]);
		}

		[TestMethod]
		public void CheckUnknownIndexer()
		{
			Assert.AreEqual("false", new LanguageWriterConfiguration()[Guid.NewGuid().ToString("N")]);
		}

		[TestMethod]
		public void CheckVisibilityValues()
		{
			var visibility = new LanguageWriterConfiguration().Visibility;
			Assert.IsTrue(visibility.Assembly);
			Assert.IsTrue(visibility.Family);
			Assert.IsTrue(visibility.FamilyAndAssembly);
			Assert.IsTrue(visibility.FamilyOrAssembly);
			Assert.IsTrue(visibility.Private);
			Assert.IsTrue(visibility.Public);
		}
	}
}
