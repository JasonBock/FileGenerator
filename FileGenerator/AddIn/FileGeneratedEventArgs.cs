using Spackle.Extensions;
using System;

namespace FileGenerator.AddIn
{
	internal sealed class FileGeneratedEventArgs : EventArgs
	{
		private const string ErrorEmptyFileName = "The given file name must contain information.";

		internal FileGeneratedEventArgs(string fileName)
			: base()
		{
			fileName.CheckParameterForNull("fileName");

			if(fileName.Length == 0)
			{
				throw new ArgumentException(FileGeneratedEventArgs.ErrorEmptyFileName, "fileName");
			}

			this.FileName = fileName;
		}

		internal string FileName
		{
			get;
			private set;
		}
	}
}
