using FileGenerator.AddIn;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spackle.Testing;
using System;

namespace FileGenerator.Tests.AddIn
{
	[TestClass]
	public sealed class FileGeneratedEventArgsTests : CoreTests
	{
		[TestMethod]
		public void Create()
		{
			const string FileName = "Assembly.dll";
			Assert.AreEqual(FileName, 
				new FileGeneratedEventArgs(FileName).FileName);
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void CreateWithFileName()
		{
			new FileGeneratedEventArgs(string.Empty);
		}

		[TestMethod, ExpectedException(typeof(ArgumentNullException))]
		public void CreateWithNullFileName()
		{
			new FileGeneratedEventArgs(null);
		}
	}
}
