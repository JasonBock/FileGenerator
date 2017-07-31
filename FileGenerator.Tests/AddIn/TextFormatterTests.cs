using FileGenerator.AddIn;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spackle.Testing;
using System;

namespace FileGenerator.Tests.AddIn
{
	[TestClass]
	public sealed class TextFormatterTests : CoreTests
	{
		[TestMethod]
		public void Write()
		{
			using(var formatter = new TextFormatter())
			{
				formatter.Write("This text");
				formatter.Write("That text");
				Assert.AreEqual("This textThat text", formatter.ToString());
			}
		}

		[TestMethod, ExpectedException(typeof(ObjectDisposedException))]
		public void WriteAfterDisposed()
		{
			TextFormatter formatter = null;
			
			using(formatter = new TextFormatter())
			{
				formatter.Write("This text");
			}
			
			formatter.Write("no");
		}

		[TestMethod]
		public void WriteLine()
		{
			using(var formatter = new TextFormatter())
			{
				formatter.Write("This text");
				formatter.WriteLine();
				formatter.Write("That text");
				Assert.AreEqual("This text" + Environment.NewLine + "That text", formatter.ToString());
			}
		}

		[TestMethod, ExpectedException(typeof(ObjectDisposedException))]
		public void WriteLineAfterDisposed()
		{
			TextFormatter formatter = null;

			using(formatter = new TextFormatter())
			{
				formatter.WriteLine();
			}

			formatter.WriteLine();
		}

		[TestMethod]
		public void WriteComment()
		{
			using(var formatter = new TextFormatter())
			{
				formatter.WriteComment("This text");
				Assert.AreEqual("This text", formatter.ToString());
			}
		}

		[TestMethod, ExpectedException(typeof(ObjectDisposedException))]
		public void WriteCommentAfterDisposed()
		{
			TextFormatter formatter = null;

			using(formatter = new TextFormatter())
			{
				formatter.WriteComment("comment");
			}

			formatter.WriteComment("comment");
		}

		[TestMethod]
		public void WriteDeclaration()
		{
			using(var formatter = new TextFormatter())
			{
				formatter.WriteDeclaration("This text");
				Assert.AreEqual("This text", formatter.ToString());
			}
		}

		[TestMethod, ExpectedException(typeof(ObjectDisposedException))]
		public void WriteDeclarationAfterDisposed()
		{
			TextFormatter formatter = null;

			using(formatter = new TextFormatter())
			{
				formatter.WriteDeclaration("comment");
			}

			formatter.WriteDeclaration("comment");
		}

		[TestMethod]
		public void WriteDeclarationWithObject()
		{
			using(var formatter = new TextFormatter())
			{
				formatter.WriteDeclaration("This text", "x");
				Assert.AreEqual("This text", formatter.ToString());
			}
		}

		[TestMethod, ExpectedException(typeof(ObjectDisposedException))]
		public void WriteDeclarationWithObjectAfterDisposed()
		{
			TextFormatter formatter = null;

			using(formatter = new TextFormatter())
			{
				formatter.WriteDeclaration("comment", "x");
			}

			formatter.WriteDeclaration("comment", "x");
		}

		[TestMethod]
		public void WriteKeyword()
		{
			using(var formatter = new TextFormatter())
			{
				formatter.WriteKeyword("class");
				Assert.AreEqual("class", formatter.ToString());
			}
		}

		[TestMethod, ExpectedException(typeof(ObjectDisposedException))]
		public void WriteKeywordAfterDisposed()
		{
			TextFormatter formatter = null;

			using(formatter = new TextFormatter())
			{
				formatter.WriteKeyword("comment");
			}

			formatter.WriteKeyword("comment");
		}

		[TestMethod]
		public void WriteLiteral()
		{
			using(var formatter = new TextFormatter())
			{
				formatter.WriteLiteral("This text");
				Assert.AreEqual("This text", formatter.ToString());
			}
		}

		[TestMethod, ExpectedException(typeof(ObjectDisposedException))]
		public void WriteLiteralAfterDisposed()
		{
			TextFormatter formatter = null;

			using(formatter = new TextFormatter())
			{
				formatter.WriteLiteral("comment");
			}

			formatter.WriteLiteral("comment");
		}

		[TestMethod]
		public void WriteProperty()
		{
			using(var formatter = new TextFormatter())
			{
				formatter.WriteProperty("property", "value");
				Assert.AreEqual(string.Empty, formatter.ToString());
			}
		}

		[TestMethod, ExpectedException(typeof(ObjectDisposedException))]
		public void WritePropertyAfterDisposed()
		{
			TextFormatter formatter = null;

			using(formatter = new TextFormatter())
			{
				formatter.WriteProperty("property", "value");
			}

			formatter.WriteProperty("property", "value");
		}

		[TestMethod, ExpectedException(typeof(NotSupportedException))]
		public void WritePropertyWithPropertiesAllowed()
		{
			using(var formatter = new TextFormatter())
			{
				formatter.AllowProperties = true;
				formatter.WriteProperty("property", "value");
			}
		}

		[TestMethod]
		public void WriteReference()
		{
			using(var formatter = new TextFormatter())
			{
				formatter.WriteReference("reference", "tooltip", this);
				Assert.AreEqual("reference", formatter.ToString());
			}
		}

		[TestMethod, ExpectedException(typeof(ObjectDisposedException))]
		public void WriteReferenceAfterDisposed()
		{
			TextFormatter formatter = null;

			using(formatter = new TextFormatter())
			{
				formatter.WriteReference("reference", "tooltip", this);
			}

			formatter.WriteReference("reference", "tooltip", this);
		}

		[TestMethod]
		public void WriteWithIndentAndOutdent()
		{
			using(var formatter = new TextFormatter())
			{
				formatter.Write("if(x == 3)");
				formatter.WriteLine();
				formatter.Write("{");
				formatter.WriteLine();
				formatter.WriteIndent();
				formatter.Write("return true;");
				formatter.WriteLine();
				formatter.WriteOutdent();
				formatter.Write("}");
				formatter.WriteLine();
				Assert.AreEqual(
					"if(x == 3)" + Environment.NewLine + 
					"{" + Environment.NewLine + 
					"    return true;" + Environment.NewLine +
					"}" + Environment.NewLine, 
					formatter.ToString());
			}
		}

		[TestMethod, ExpectedException(typeof(ObjectDisposedException))]
		public void WriteIndentAfterDisposed()
		{
			TextFormatter formatter = null;

			using(formatter = new TextFormatter())
			{
				formatter.WriteIndent();
			}

			formatter.WriteIndent();
		}

		[TestMethod, ExpectedException(typeof(ObjectDisposedException))]
		public void WriteOutdentAfterDisposed()
		{
			TextFormatter formatter = null;

			using(formatter = new TextFormatter())
			{
				formatter.WriteOutdent();
			}

			formatter.WriteOutdent();
		}
	}
}
