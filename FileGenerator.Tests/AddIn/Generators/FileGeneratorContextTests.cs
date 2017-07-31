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
	public sealed class FileGeneratorContextTests : CoreTests
	{
		[TestMethod]
		public void Create()
		{
			const string Directory = @"C:\Windows";
			var language = new MockLanguage();
			var translator = new MockTranslator();

			using(var @event = new ManualResetEvent(false))
			{
				var context = new FileGeneratorContext<FileGeneratorContextTests>(
					this, Directory, language, translator,
					@event, true, true);
				Assert.AreEqual(@event, context.Cancel);
				Assert.IsTrue(context.CreateSubdirectories);
				Assert.IsTrue(context.CreateVsNetProject);
				Assert.AreEqual(Directory, context.Directory);
				Assert.IsFalse(context.IsRoot);
				Assert.AreEqual(this, context.Item);
				Assert.AreEqual(language, context.Language);
				Assert.AreEqual(translator, context.Translator);
			}
		}

		[TestMethod]
		public void CreateWithIsRootAsFalse()
		{
			const string Directory = @"C:\Windows";
			var language = new MockLanguage();
			var translator = new MockTranslator();

			using(var @event = new ManualResetEvent(false))
			{
				var context = new FileGeneratorContext<FileGeneratorContextTests>(
					this, Directory, language, translator,
					@event, true, true, false);
				Assert.AreEqual(@event, context.Cancel);
				Assert.IsTrue(context.CreateSubdirectories);
				Assert.IsTrue(context.CreateVsNetProject);
				Assert.AreEqual(Directory, context.Directory);
				Assert.IsFalse(context.IsRoot);
				Assert.AreEqual(this, context.Item);
				Assert.AreEqual(language, context.Language);
				Assert.AreEqual(translator, context.Translator);
			}
		}

		[TestMethod]
		public void CreateWithIsRootAsTrue()
		{
			const string Directory = @"C:\Windows";
			var language = new MockLanguage();
			var translator = new MockTranslator();

			using(var @event = new ManualResetEvent(false))
			{
				var context = new FileGeneratorContext<FileGeneratorContextTests>(
					this, Directory, language, translator,
					@event, true, true, true);
				Assert.AreEqual(@event, context.Cancel);
				Assert.IsTrue(context.CreateSubdirectories);
				Assert.IsTrue(context.CreateVsNetProject);
				Assert.AreEqual(Directory, context.Directory);
				Assert.IsTrue(context.IsRoot);
				Assert.AreEqual(this, context.Item);
				Assert.AreEqual(language, context.Language);
				Assert.AreEqual(translator, context.Translator);
			}
		}

		[TestMethod]
		public void CreateWithDirectoryThatEndsWithSeparator()
		{
			const string Directory = @"C:\Windows";

			using(var @event = new ManualResetEvent(false))
			{
				var context = new FileGeneratorContext<FileGeneratorContextTests>(
					this, @"C:\Windows" + Path.DirectorySeparatorChar,
					new MockLanguage(), new MockTranslator(),
					@event, true, true);
					
				Assert.AreEqual(Directory, context.Directory);
			}
		}

		[TestMethod, ExpectedException(typeof(ArgumentNullException))]
		public void CreateWithNullDirectory()
		{
			using(var @event = new ManualResetEvent(false))
			{
				new FileGeneratorContext<FileGeneratorContextTests>(
					this, null, new MockLanguage(), new MockTranslator(),
					@event, true, true);
			}
		}

		[TestMethod, ExpectedException(typeof(DirectoryNotFoundException))]
		public void CreateWithDirectoryThatDoesNotExist()
		{
			using(var @event = new ManualResetEvent(false))
			{
				new FileGeneratorContext<FileGeneratorContextTests>(
					this, Guid.NewGuid().ToString("N"), new MockLanguage(), new MockTranslator(),
					@event, true, true);
			}
		}

		[TestMethod, ExpectedException(typeof(ArgumentNullException))]
		public void CreateWithNullLanguage()
		{
			using(var @event = new ManualResetEvent(false))
			{
				new FileGeneratorContext<FileGeneratorContextTests>(
					this, @"C:\Windows", null, new MockTranslator(),
					@event, true, true);
			}
		}

		[TestMethod, ExpectedException(typeof(ArgumentNullException))]
		public void CreateWithNullTranslator()
		{
			using(var @event = new ManualResetEvent(false))
			{
				new FileGeneratorContext<FileGeneratorContextTests>(
					this, @"C:\Windows", new MockLanguage(), null,
					@event, true, true);
			}
		}

		[TestMethod, ExpectedException(typeof(ArgumentNullException))]
		public void CreateWithNullCancel()
		{
			new FileGeneratorContext<FileGeneratorContextTests>(
				this, @"C:\Windows", new MockLanguage(), new MockTranslator(),
				null, true, true);
		}
	}
}
