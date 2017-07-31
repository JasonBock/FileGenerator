// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project. 
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc. 
//
// To add a suppression to this file, right-click the message in the 
// Error List, point to "Suppress Message(s)", and click 
// "In Project Suppression File". 
// You do not need to add suppressions to this file manually. 

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "FileGenerator.AddIn")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "FileGenerator.AddIn.UI")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames", MessageId = "serviceProvider", Scope = "member", Target = "FileGenerator.AddIn.FileGeneratorPackage.#Load(System.IServiceProvider)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member", Target = "FileGenerator.AddIn.TextFormatter.#AllowProperties")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Scope = "type", Target = "FileGenerator.AddIn.Generators.AssemblyFileGenerator")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Scope = "type", Target = "FileGenerator.AddIn.Generators.NamespaceFileGenerator")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames", MessageId = "typeCount", Scope = "member", Target = "FileGenerator.AddIn.UI.FileGeneratorControl.#SetupProgressBar(System.Int32)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Scope = "member", Target = "FileGenerator.AddIn.UI.FileGeneratorControl.#SetTargetInformation(System.Object)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Scope = "member", Target = "FileGenerator.AddIn.UI.FileGeneratorControl.#Resolve(System.Object)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames", MessageId = "assemblyBrowser", Scope = "member", Target = "FileGenerator.AddIn.UI.FileGeneratorControl.#OnGenerateFilesButtonClick(System.Object,System.EventArgs)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "FileGenerator.AddIn.Generators.TypeFileGenerator.#OnGenerate()")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "FileGenerator.AddIn.Generators")]
