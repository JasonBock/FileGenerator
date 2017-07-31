using System;
using Reflector.CodeModel;

namespace FileGenerator.Tests.AddIn.Generators
{
	internal sealed class MockNamespace : INamespace
	{
		public string Name
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public ITypeDeclarationCollection Types
		{
			get
			{
				return new MockTypeDeclarationCollection();
			}
		}
	}
}
