// ---------------------------------------------------------
// Lutz Roeder's .NET Reflector
// Copyright (c) 2000-2006 Lutz Roeder. All rights reserved.
// http://www.aisto.com/roeder
// ---------------------------------------------------------
using Reflector.CodeModel;
using System;
using System.Globalization;
using System.IO;

namespace FileGenerator.AddIn
{
	internal sealed class TextFormatter : IFormatter, IDisposable
	{
		private bool disposed;
		private int indent;
		private bool newLine;
		private StringWriter writer = new StringWriter(CultureInfo.InvariantCulture);

		~TextFormatter()
		{
			this.Dispose(false);
		}
		
		private void ApplyIndent()
		{
			this.CheckForDisposed();
			
			if(this.newLine)
			{
				for(int i = 0; i < this.indent; i++)
				{
					this.writer.Write("    ");
				}

				this.newLine = false;
			}
		}

		private void CheckForDisposed()
		{
			if(this.disposed)
			{
				throw new ObjectDisposedException("TextFormatter");
			}
		}
		
		public void Dispose()
		{
			this.CheckForDisposed();
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if(!this.disposed)
			{
				if(disposing)
				{
					this.writer.Dispose();
				}
				
				this.disposed = true;
			}
		}

		public override string ToString()
		{
			this.CheckForDisposed();
			return this.writer.ToString();
		}

		public void Write(string text)
		{
			this.CheckForDisposed();
			this.ApplyIndent();
			this.writer.Write(text);
		}

		public void WriteDeclaration(string text)
		{
			this.CheckForDisposed();
			this.WriteBold(text);
		}

		public void WriteDeclaration(string value, object target)
		{
			this.CheckForDisposed();
			this.Write(value);
		}

		public void WriteComment(string text)
		{
			this.CheckForDisposed();
			this.WriteText(text);
		}

		public void WriteLiteral(string text)
		{
			this.CheckForDisposed();
			this.WriteText(text);
		}

		public void WriteKeyword(string text)
		{
			this.CheckForDisposed();
			this.WriteText(text);
		}

		public void WriteIndent()
		{
			this.CheckForDisposed();
			this.indent++;
		}

		public void WriteLine()
		{
			this.CheckForDisposed();
			this.writer.WriteLine();
			this.newLine = true;
		}

		public void WriteOutdent()
		{
			this.CheckForDisposed();
			this.indent--;
		}

		public void WriteReference(string text, string toolTip, Object reference)
		{
			this.CheckForDisposed();
			this.ApplyIndent();
			this.writer.Write(text);
		}

		public void WriteProperty(string propertyName, string propertyValue)
		{
			this.CheckForDisposed();
			if(this.AllowProperties)
			{
				throw new NotSupportedException();
			}
		}

		private void WriteBold(string text)
		{
			this.CheckForDisposed();
			this.ApplyIndent();
			this.writer.Write(text);
		}

		private void WriteText(string text)
		{
			this.CheckForDisposed();
			this.ApplyIndent();
			this.writer.Write(text);
		}

		public bool AllowProperties
		{
			get;
			set;
		}
	}
}
