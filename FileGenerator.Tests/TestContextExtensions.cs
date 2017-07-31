using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace FileGenerator.Tests
{
	internal static class TestContextExtensions
	{
		internal static string GetOutputPath(this TestContext @this, string testClassTypeName)
		{
			return Path.Combine(@this.TestDeploymentDir,
				testClassTypeName + Path.DirectorySeparatorChar + @this.TestName);		
		}
	}
}
