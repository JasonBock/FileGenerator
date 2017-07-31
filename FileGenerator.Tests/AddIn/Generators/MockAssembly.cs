using Reflector.CodeModel;
using System;

namespace FileGenerator.Tests.AddIn.Generators
{
	internal sealed class MockAssembly : IAssembly
	{
		public int CompareTo(object obj)
		{
			throw new NotImplementedException();
		}

		public IAssembly Resolve()
		{
			throw new NotImplementedException();
		}

		public IAssemblyManager AssemblyManager
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

		public ICustomAttributeCollection Attributes
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public IMethodDeclaration EntryPoint
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

		public HashAlgorithm HashAlgorithm
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

		public IModuleCollection Modules
		{
			get
			{
				return new MockModuleCollection();
			}
		}

		public IResourceCollection Resources
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public string Status
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public AssemblyType Type
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

		public string Culture
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

		public byte[] HashValue
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

		public byte[] PublicKey
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

		public byte[] PublicKeyToken
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

		public bool Retargetable
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

		public Version Version
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
	}
}
