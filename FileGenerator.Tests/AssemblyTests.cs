using FileGenerator.AddIn.Generators;
using FileGenerator.Tests.AddIn.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reflector;
using Reflector.CodeModel;
using Reflector.CodeModel.Memory;
using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace FileGenerator.Tests
{
	[TestClass]
	public static class AssemblyTests
	{
		private const string FrameworkDirectory = @"%SystemRoot%\Microsoft.net\Framework";
		private const string ReferenceAssembliesDirectory = @"%ProgramFiles%\Reference Assemblies";
		public const string TestsAssemblyFileName = "FileGenerator.Tests.dll";
		public const string TestsTargetAssemblyFileName = "FileGenerator.Tests.Target.dll";

		[AssemblyInitialize]
		public static void AssemblyInitialize(TestContext context)
		{
			AssemblyTests.ServiceProvider = new ApplicationManager(null);
			var cache = AssemblyTests.ServiceProvider.GetService(
				typeof(IAssemblyCache)) as IAssemblyCache;
			cache.Directories.Clear();

			cache.Directories.Add(
				Environment.ExpandEnvironmentVariables(AssemblyTests.FrameworkDirectory));
			cache.Directories.Add(
				Environment.ExpandEnvironmentVariables(AssemblyTests.ReferenceAssembliesDirectory));

			AssemblyTests.Manager = AssemblyTests.ServiceProvider.GetService(
				typeof(IAssemblyManager)) as IAssemblyManager;

			AssemblyTests.Manager.Symbols = true;
			AssemblyTests.Manager.Resolver = new AssemblyResolver(AssemblyTests.Manager, cache);
			AssemblyTests.Manager.LoadFile(AssemblyTests.TestsAssemblyFileName);
			AssemblyTests.Manager.LoadFile(AssemblyTests.TestsTargetAssemblyFileName);
			AssemblyTests.Initialize(context.TestDeploymentDir);
		}

		internal static IAssembly GetAssembly(string assemblyName)
		{
			return (from IAssembly assembly in AssemblyTests.Manager.Assemblies
					  where assembly.Name == assemblyName
					  select assembly).FirstOrDefault();
		}

		internal static ILanguage GetLanguage(string name)
		{
			return (from ILanguage language in
						  ((ILanguageManager)AssemblyTests.ServiceProvider.GetService(typeof(ILanguageManager))).Languages
					  where language.Name == name
					  select language).FirstOrDefault();
		}

		internal static IModule GetModule(string assemblyName)
		{
			return (from IAssembly assembly in AssemblyTests.Manager.Assemblies
					  where assembly.Name == assemblyName
					  select assembly.Modules[0]).FirstOrDefault();
		}

		internal static INamespace GetNamespace(string assemblyName, string @namespace)
		{
			var value = new Namespace()
			{
				Name = @namespace
			};

			var types = from IAssembly assembly in AssemblyTests.Manager.Assemblies
							where assembly.Name == assemblyName
							from ITypeDeclaration type in assembly.Modules[0].Types
							where type.Namespace == @namespace
							select type;

			foreach(var type in types)
			{
				value.Types.Add(type);
			}

			return value;
		}

		internal static ITranslator GetTranslator()
		{
			return ((ITranslatorManager)AssemblyTests.ServiceProvider.GetService(
				typeof(ITranslatorManager))).CreateDisassembler(null, null);
		}

		internal static ITypeDeclaration GetType(string assemblyName, string typeName, string typeNamespace)
		{
			return (from IAssembly assembly in AssemblyTests.Manager.Assemblies
					  where assembly.Name == assemblyName
					  from ITypeDeclaration type in assembly.Modules[0].Types
					  where type.Name == typeName
					  where type.Namespace == typeNamespace
					  select type).FirstOrDefault();
		}

		private static void Initialize(string deploymentDirectory)
		{
			var item = AssemblyTests.GetType(
				"FileGenerator.Tests.Target", "InBaseNamespace", "FileGenerator.Tests.Target");
			var outputPath = Path.Combine(deploymentDirectory, @"AssemblyTests\Initialize");
			Directory.CreateDirectory(outputPath);

			using(var @event = new ManualResetEvent(false))
			{
				FileGeneratorFactory.Create(
					item, outputPath,
					AssemblyTests.GetTranslator(),
					AssemblyTests.GetLanguage(LanguageInformation.CSharp),
					@event, true, true).Generate();
			}
		}

		private sealed class AssemblyResolver : IAssemblyResolver
		{
			private IAssemblyCache cache;
			private IAssemblyManager manager;

			public AssemblyResolver(IAssemblyManager manager, IAssemblyCache cache)
			{
				this.manager = manager;
				this.cache = cache;
			}

			public IAssembly Resolve(IAssemblyReference assemblyReference, string localPath)
			{
				return this.manager.LoadFile(
					this.cache.QueryLocation(assemblyReference, localPath));
			}
		}

		internal static IAssemblyManager Manager
		{
			get;
			private set;
		}

		internal static IServiceProvider ServiceProvider
		{
			get;
			private set;
		}
	}
}
