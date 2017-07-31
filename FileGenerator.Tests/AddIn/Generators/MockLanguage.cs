using Reflector.CodeModel;
using System;

namespace FileGenerator.Tests.AddIn.Generators
{
	internal sealed class MockLanguage : ILanguage
	{
		public ILanguageWriter GetWriter(IFormatter formatter, ILanguageWriterConfiguration configuration)
		{
			throw new NotImplementedException();
		}

		public string FileExtension
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public string Name
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public bool Translate
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
