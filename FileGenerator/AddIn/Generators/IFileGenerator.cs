using System;
using System.Collections.Generic;

namespace FileGenerator.AddIn.Generators
{
	internal delegate void FileCreatedEventHandler(object sender, FileGeneratedEventArgs e);

	internal interface IFileGenerator
	{
		event FileCreatedEventHandler FileCreated;

		void Generate();

		int TypeCount
		{
			get;
		}
	}
}
