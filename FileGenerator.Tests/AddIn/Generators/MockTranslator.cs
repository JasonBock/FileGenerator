using Reflector;
using Reflector.CodeModel;
using System;

namespace FileGenerator.Tests.AddIn.Generators
{
	internal sealed class MockTranslator : ITranslator
	{
		public IAssembly TranslateAssembly(IAssembly value, bool moduleList)
		{
			throw new NotImplementedException();
		}

		public IAssemblyReference TranslateAssemblyReference(IAssemblyReference value)
		{
			throw new NotImplementedException();
		}

		public IEventDeclaration TranslateEventDeclaration(IEventDeclaration value)
		{
			throw new NotImplementedException();
		}

		public IFieldDeclaration TranslateFieldDeclaration(IFieldDeclaration value)
		{
			throw new NotImplementedException();
		}

		public IMethodDeclaration TranslateMethodDeclaration(IMethodDeclaration value)
		{
			throw new NotImplementedException();
		}

		public IModule TranslateModule(IModule value, bool typeDeclarationList)
		{
			throw new NotImplementedException();
		}

		public IModuleReference TranslateModuleReference(IModuleReference value)
		{
			throw new NotImplementedException();
		}

		public INamespace TranslateNamespace(INamespace value, bool memberDeclarationList)
		{
			throw new NotImplementedException();
		}

		public IPropertyDeclaration TranslatePropertyDeclaration(IPropertyDeclaration value)
		{
			throw new NotImplementedException();
		}

		public ITypeDeclaration TranslateTypeDeclaration(ITypeDeclaration value, bool memberDeclarationList, bool methodDeclarationBody)
		{
			throw new NotImplementedException();
		}
	}
}
