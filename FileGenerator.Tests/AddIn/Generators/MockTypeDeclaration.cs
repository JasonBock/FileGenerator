using System;
using Reflector.CodeModel;

namespace FileGenerator.Tests.AddIn.Generators
{
	internal sealed class MockTypeDeclaration : ITypeDeclaration
	{
		public bool Abstract
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

		public ITypeReference BaseType
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

		public bool BeforeFieldInit
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

		public IEventDeclarationCollection Events
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public IFieldDeclarationCollection Fields
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public bool Interface
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

		public ITypeReferenceCollection Interfaces
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public IMethodDeclarationCollection Methods
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public ITypeDeclarationCollection NestedTypes
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public IPropertyDeclarationCollection Properties
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public bool RuntimeSpecialName
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

		public bool Sealed
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

		public bool SpecialName
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

		public TypeVisibility Visibility
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

		public ITypeReference GenericType
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
				return this.GetType().Name;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public string Namespace
		{
			get
			{
				return this.GetType().Namespace;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public object Owner
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

		public ITypeDeclaration Resolve()
		{
			throw new NotImplementedException();
		}

		public bool ValueType
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

		public int CompareTo(object obj)
		{
			throw new NotImplementedException();
		}

		public ITypeCollection GenericArguments
		{
			get
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

		public string Documentation
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
