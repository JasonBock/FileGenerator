using Reflector.CodeModel;
using System;

namespace FileGenerator.Tests.AddIn.Generators
{
	internal sealed class MockModule : IModule
	{
		public IAssembly Assembly
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

		public IAssemblyReferenceCollection AssemblyReferences
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public int FileAlignment
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

		public string Location
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

		public IModuleReferenceCollection ModuleReferences
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public string TargetRuntimeVersion
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

		public IUnmanagedResourceCollection UnmanagedResources
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public Guid Version
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

		public IModule Context
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

		public IModule Resolve()
		{
			throw new NotImplementedException();
		}

		public int CompareTo(object obj)
		{
			throw new NotImplementedException();
		}

		public ICustomAttributeCollection Attributes
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
