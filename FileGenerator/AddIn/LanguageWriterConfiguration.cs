using Reflector.CodeModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileGenerator.AddIn
{
	public class LanguageWriterConfiguration : ILanguageWriterConfiguration
	{
		public LanguageWriterConfiguration()
		{
			this.Visibility = new VisibilityConfiguration();
		}

		public IVisibilityConfiguration Visibility
		{
			get;
			private set;
		}

		public string this[string name]
		{
			get
			{
				switch(name)
				{
					case "ShowDocumentation":
					case "ShowCustomAttributes":
					case "ShowNamespaceImports":
					case "ShowNamespaceBody":
					case "ShowTypeDeclarationBody":
					case "ShowMethodDeclarationBody":
						return "true";
				}

				return "false";
			}
		}

		private class VisibilityConfiguration : IVisibilityConfiguration
		{
			public bool Assembly
			{
				get
				{
					return true;
				}
			}

			public bool Family
			{
				get
				{
					return true;
				}
			}

			public bool FamilyAndAssembly
			{
				get
				{
					return true;
				}
			}

			public bool FamilyOrAssembly
			{
				get
				{
					return true;
				}
			}

			public bool Private
			{
				get
				{
					return true;
				}
			}

			public bool Public
			{
				get
				{
					return true;
				}
			}
		}
	}
}
