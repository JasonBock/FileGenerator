using FileGenerator.AddIn.Generators;
using Reflector;
using Reflector.CodeModel;
using Spackle;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace FileGenerator.AddIn.UI
{
	public class FileGeneratorControl : UserControl
	{
		private delegate void FileGenerationCompleteHandler();
		private delegate void FileGeneratedHandler(FileGeneratedEventArgs fileInfo);
		private delegate void SetTargetInformationHandler(object activeItem);
		private delegate void SetupProgressBarHandler(int typeCount);

		private const string FolderDialogDescription = "Select the folder that will contain the code files.";

		private IAssemblyBrowser assemblyBrowser;
		private Button browseDirectoriesButton;
		private ManualResetEvent cancel;
		private Button cancelGenerationButton;
		private ManualResetEvent complete;
		private CheckBox createSubDirectories;
		private CheckBox createVisualStudioProjectFile;
		private ProgressBar fileGenerationProgress;
		private Button generateFilesButton;
		private Label outputDirectoryLabel;
		private TextBox outputDirectoryText;
		private IServiceProvider serviceProvider;
		private Label targetLabel;
		private int typeCount;
		private TextBox fileGenerationStatusText;
		private int typesGenerated;

		public FileGeneratorControl()
			: base()
		{
			this.InitializeComponent();
			this.cancelGenerationButton.Enabled = false;
		}

		private void CancelFileGeneration()
		{
			if(this.cancel != null && this.complete != null)
			{
				this.cancel.Set();

				var finished = false;

				do
				{
					finished = this.complete.WaitOne(FileGeneratorFactory.EventWaitTime, false);
					Application.DoEvents();
				} while(!finished);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if(disposing)
			{
				this.CancelFileGeneration();
				this.cancel.Close();
				this.complete.Close();
			}

			base.Dispose(disposing);
		}

		public FileGeneratorControl(IServiceProvider serviceProvider)
			: this()
		{
			this.serviceProvider = serviceProvider;
			this.assemblyBrowser = (IAssemblyBrowser)this.serviceProvider.GetService(typeof(IAssemblyBrowser));
			this.assemblyBrowser.ActiveItemChanged += new EventHandler(OnAssemblyBrowserActiveItemChanged);
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.outputDirectoryLabel = new System.Windows.Forms.Label();
			this.outputDirectoryText = new System.Windows.Forms.TextBox();
			this.browseDirectoriesButton = new System.Windows.Forms.Button();
			this.fileGenerationProgress = new System.Windows.Forms.ProgressBar();
			this.generateFilesButton = new System.Windows.Forms.Button();
			this.cancelGenerationButton = new System.Windows.Forms.Button();
			this.targetLabel = new System.Windows.Forms.Label();
			this.createSubDirectories = new System.Windows.Forms.CheckBox();
			this.createVisualStudioProjectFile = new System.Windows.Forms.CheckBox();
			this.fileGenerationStatusText = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// outputDirectoryLabel
			// 
			this.outputDirectoryLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.outputDirectoryLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.outputDirectoryLabel.Location = new System.Drawing.Point(8, 12);
			this.outputDirectoryLabel.Name = "outputDirectoryLabel";
			this.outputDirectoryLabel.Size = new System.Drawing.Size(88, 16);
			this.outputDirectoryLabel.TabIndex = 0;
			this.outputDirectoryLabel.Text = "Output Directory:";
			this.outputDirectoryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// outputDirectoryText
			// 
			this.outputDirectoryText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.outputDirectoryText.Location = new System.Drawing.Point(96, 8);
			this.outputDirectoryText.Name = "outputDirectoryText";
			this.outputDirectoryText.Size = new System.Drawing.Size(464, 21);
			this.outputDirectoryText.TabIndex = 1;
			// 
			// browseDirectoriesButton
			// 
			this.browseDirectoriesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.browseDirectoriesButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.browseDirectoriesButton.Location = new System.Drawing.Point(568, 8);
			this.browseDirectoriesButton.Name = "browseDirectoriesButton";
			this.browseDirectoriesButton.Size = new System.Drawing.Size(75, 23);
			this.browseDirectoriesButton.TabIndex = 2;
			this.browseDirectoriesButton.Text = "Browse...";
			this.browseDirectoriesButton.Click += new System.EventHandler(this.OnBrowseDirectoriesButtonClick);
			// 
			// fileGenerationProgress
			// 
			this.fileGenerationProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.fileGenerationProgress.Location = new System.Drawing.Point(8, 220);
			this.fileGenerationProgress.Name = "fileGenerationProgress";
			this.fileGenerationProgress.Size = new System.Drawing.Size(632, 23);
			this.fileGenerationProgress.TabIndex = 9;
			// 
			// generateFilesButton
			// 
			this.generateFilesButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.generateFilesButton.Location = new System.Drawing.Point(8, 62);
			this.generateFilesButton.Name = "generateFilesButton";
			this.generateFilesButton.Size = new System.Drawing.Size(88, 23);
			this.generateFilesButton.TabIndex = 5;
			this.generateFilesButton.Text = "Generate Files";
			this.generateFilesButton.Click += new System.EventHandler(this.OnGenerateFilesButtonClick);
			// 
			// cancelGenerationButton
			// 
			this.cancelGenerationButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cancelGenerationButton.Location = new System.Drawing.Point(104, 62);
			this.cancelGenerationButton.Name = "cancelGenerationButton";
			this.cancelGenerationButton.Size = new System.Drawing.Size(88, 23);
			this.cancelGenerationButton.TabIndex = 6;
			this.cancelGenerationButton.Text = "Cancel";
			this.cancelGenerationButton.Click += new System.EventHandler(this.OnCancelGenerationButtonClick);
			// 
			// targetLabel
			// 
			this.targetLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.targetLabel.Location = new System.Drawing.Point(9, 93);
			this.targetLabel.Name = "targetLabel";
			this.targetLabel.Size = new System.Drawing.Size(632, 16);
			this.targetLabel.TabIndex = 8;
			// 
			// createSubDirectories
			// 
			this.createSubDirectories.AutoSize = true;
			this.createSubDirectories.Location = new System.Drawing.Point(8, 39);
			this.createSubDirectories.Name = "createSubDirectories";
			this.createSubDirectories.Size = new System.Drawing.Size(130, 17);
			this.createSubDirectories.TabIndex = 3;
			this.createSubDirectories.Text = "Create Subdirectories";
			this.createSubDirectories.UseVisualStyleBackColor = true;
			// 
			// createVisualStudioProjectFile
			// 
			this.createVisualStudioProjectFile.AutoSize = true;
			this.createVisualStudioProjectFile.Location = new System.Drawing.Point(143, 39);
			this.createVisualStudioProjectFile.Name = "createVisualStudioProjectFile";
			this.createVisualStudioProjectFile.Size = new System.Drawing.Size(178, 17);
			this.createVisualStudioProjectFile.TabIndex = 4;
			this.createVisualStudioProjectFile.Text = "Create Visual Studio Project File";
			this.createVisualStudioProjectFile.UseVisualStyleBackColor = true;
			// 
			// fileGenerationStatusText
			// 
			this.fileGenerationStatusText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
							| System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.fileGenerationStatusText.Location = new System.Drawing.Point(8, 123);
			this.fileGenerationStatusText.MaxLength = 0;
			this.fileGenerationStatusText.Multiline = true;
			this.fileGenerationStatusText.Name = "fileGenerationStatusText";
			this.fileGenerationStatusText.ReadOnly = true;
			this.fileGenerationStatusText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.fileGenerationStatusText.Size = new System.Drawing.Size(632, 86);
			this.fileGenerationStatusText.TabIndex = 10;
			// 
			// FileGeneratorControl
			// 
			this.Controls.Add(this.fileGenerationStatusText);
			this.Controls.Add(this.createVisualStudioProjectFile);
			this.Controls.Add(this.createSubDirectories);
			this.Controls.Add(this.targetLabel);
			this.Controls.Add(this.cancelGenerationButton);
			this.Controls.Add(this.generateFilesButton);
			this.Controls.Add(this.fileGenerationProgress);
			this.Controls.Add(this.browseDirectoriesButton);
			this.Controls.Add(this.outputDirectoryText);
			this.Controls.Add(this.outputDirectoryLabel);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "FileGeneratorControl";
			this.Size = new System.Drawing.Size(648, 252);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private static object Resolve(object activeItem)
		{
			IAssembly assemblyToResolve = null;
			bool allAreResolved = true;

			if(activeItem is IAssembly)
			{
				assemblyToResolve = (IAssembly)activeItem;
			}
			else if(activeItem is IModule)
			{
				assemblyToResolve = ((IModule)activeItem).Assembly;
			}
			else if(activeItem is INamespace)
			{
				var namespaceActiveItem = (INamespace)activeItem;

				if(namespaceActiveItem.Types != null && namespaceActiveItem.Types.Count > 0)
				{
					assemblyToResolve = ((IModule)namespaceActiveItem.Types[0].Owner).Assembly;
				}
			}
			else if(activeItem is ITypeDeclaration)
			{
				var typeActiveItem = (ITypeDeclaration)activeItem;
				assemblyToResolve = ((IModule)typeActiveItem.Owner).Assembly;
			}

			if(assemblyToResolve != null)
			{
				foreach(IModule module in assemblyToResolve.Modules)
				{
					foreach(IAssemblyReference assemblyReferenceName in module.AssemblyReferences)
					{
						var resolvedAssembly = assemblyReferenceName.Resolve();

						if(resolvedAssembly == null)
						{
							allAreResolved = false;
							break;
						}
					}
				}
			}

			if(allAreResolved == false)
			{
				assemblyToResolve = null;
			}

			return assemblyToResolve;
		}

		private void FileGenerated(FileGeneratedEventArgs fileInfo)
		{
			this.typesGenerated++;
			this.fileGenerationProgress.Increment(1);
			this.fileGenerationProgress.Refresh();

			this.fileGenerationStatusText.Text = fileInfo.FileName + " is generated.";
			this.fileGenerationStatusText.Refresh();
		}

		private void FileGenerationComplete()
		{
			cancelGenerationButton.Enabled = false;

			var results = new StringBuilder();

			results.Append("Total number of types: ").Append(this.typeCount).Append(Environment.NewLine)
				.Append("Total number of generated types: ").Append(this.typesGenerated);

			this.fileGenerationStatusText.Text = results.ToString();
			this.fileGenerationStatusText.SelectionStart = 0;
			this.fileGenerationStatusText.SelectionLength = 0;
			this.UpdateUIState();
		}

		private void GenerateFiles(object data)
		{
			this.cancel = new ManualResetEvent(false);
			this.complete = new ManualResetEvent(false);
			this.typesGenerated = 0;

			try
			{
				var state = data as GenerateFilesState;
				var languageManager = (ILanguageManager)this.serviceProvider.GetService(typeof(ILanguageManager));
				var language = languageManager.ActiveLanguage;
				var visitorManager = (ITranslatorManager)this.serviceProvider.GetService(typeof(ITranslatorManager));
				var visitor = visitorManager.CreateDisassembler(null, null);

				var fileGenerator = FileGeneratorFactory.Create(
					state.ActiveItem, state.Directory, visitor,
					language, this.cancel, state.CreateSubdirectories,
					state.CreateVisualStudioProjectFile);

				this.Invoke(new SetTargetInformationHandler(this.SetTargetInformation),
					 new object[] { state.ActiveItem });

				this.typeCount = fileGenerator.TypeCount;

				if(fileGenerator != null)
				{
					fileGenerator.FileCreated += this.OnFileGenerated;

					this.Invoke(new SetupProgressBarHandler(this.SetupProgressBar),
						 new object[] { fileGenerator.TypeCount });
					
					try
					{
						fileGenerator.Generate();					
					}
					finally
					{
						fileGenerator.FileCreated -= this.OnFileGenerated;					
					}
				}
			}
			finally
			{
				this.complete.Set();
				this.Invoke(new FileGenerationCompleteHandler(this.FileGenerationComplete));
			}
		}

		private void OnAssemblyBrowserActiveItemChanged(object sender, EventArgs e)
		{
			this.UpdateUIState();
		}

		private void OnBrowseDirectoriesButtonClick(object sender, EventArgs e)
		{
			using(var folderDialog = new FolderBrowserDialog())
			{
				folderDialog.Description = FolderDialogDescription;
				folderDialog.ShowNewFolderButton = true;

				if(Directory.Exists(this.outputDirectoryText.Text) == true)
				{
					folderDialog.SelectedPath = this.outputDirectoryText.Text;
				}
				else
				{
					folderDialog.RootFolder = Environment.SpecialFolder.MyComputer;
				}

				var folderResult = folderDialog.ShowDialog(this.ParentForm);

				if(folderResult == DialogResult.OK)
				{
					this.outputDirectoryText.Text = folderDialog.SelectedPath;
				}
			}
		}

		private void OnCancelGenerationButtonClick(object sender, EventArgs e)
		{
			using(var switcher = new ScopeSwitcher<Control, Cursor>(this.Parent, Cursors.WaitCursor))
			{
				this.CancelFileGeneration();
				this.FileGenerationComplete();
			}
		}

		private void OnFileGenerated(object sender, FileGeneratedEventArgs fileInfo)
		{
			this.Invoke(new FileGeneratedHandler(this.FileGenerated), new object[] { fileInfo });
		}

		private void OnGenerateFilesButtonClick(object sender, EventArgs e)
		{
			if(this.outputDirectoryText.Text != null && this.outputDirectoryText.Text.Trim().Length > 0)
			{
				if(Directory.Exists(this.outputDirectoryText.Text) == false)
				{
					Directory.CreateDirectory(this.outputDirectoryText.Text);
				}

				using(var switcher = new ScopeSwitcher<Control, Cursor>(this.Parent, Cursors.WaitCursor))
				{
					var assemblyBrowser = (IAssemblyBrowser)this.serviceProvider.GetService(typeof(IAssemblyBrowser));

					if(assemblyBrowser.ActiveItem != null)
					{
						object resolvedObject = FileGeneratorControl.Resolve(assemblyBrowser.ActiveItem);

						if(resolvedObject != null)
						{
							if(ThreadPool.QueueUserWorkItem(new WaitCallback(this.GenerateFiles),
								new GenerateFilesState()
								{
									ActiveItem = assemblyBrowser.ActiveItem,
									CreateSubdirectories = this.createSubDirectories.Checked,
									CreateVisualStudioProjectFile = this.createVisualStudioProjectFile.Checked,
									Directory = this.outputDirectoryText.Text
								}))
							{
								this.generateFilesButton.Enabled = false;
								this.cancelGenerationButton.Enabled = true;
							}
						}
						else
						{
							this.fileGenerationStatusText.Text = string.Format(
								CultureInfo.CurrentCulture, "Could not resolve the active item type {0}.",
								assemblyBrowser.ActiveItem.GetType().FullName);
							this.fileGenerationStatusText.SelectionStart = 0;
							this.fileGenerationStatusText.SelectionLength = 0;
						}
					}
				}
			}
		}

		private void SetTargetInformation(object activeItem)
		{
			if(activeItem is IAssembly)
			{
				this.targetLabel.Text = "Assembly: " + ((IAssembly)activeItem).Name;
			}
			else if(activeItem is IModule)
			{
				this.targetLabel.Text = "Module: " + ((IModule)activeItem).Name;
			}
			else if(activeItem is INamespace)
			{
				this.targetLabel.Text = "Namespace: " + ((INamespace)activeItem).Name;
			}
			else if(activeItem is ITypeDeclaration)
			{
				var type = (ITypeDeclaration)activeItem;
				this.targetLabel.Text = string.Format(CultureInfo.CurrentCulture,
					"Type: {0}.{1}", type.Namespace, type.Name);
			}
		}

		private void SetupProgressBar(int typeCount)
		{
			this.fileGenerationProgress.Minimum = 0;
			this.fileGenerationProgress.Maximum = typeCount;
			this.fileGenerationProgress.Value = 0;
		}

		private void UpdateUIState()
		{
			if(this.complete != null &&
				this.complete.WaitOne(FileGeneratorFactory.EventWaitTime, false) == false)
			{
				this.generateFilesButton.Enabled = false;
				this.createVisualStudioProjectFile.Enabled = false;
			}
			else
			{
				if(this.Visible == true)
				{
					this.generateFilesButton.Enabled =
						 (this.assemblyBrowser.ActiveItem is IModule ||
						 this.assemblyBrowser.ActiveItem is ITypeDeclaration ||
						 this.assemblyBrowser.ActiveItem is IAssembly ||
						 this.assemblyBrowser.ActiveItem is INamespace);
					this.createVisualStudioProjectFile.Enabled = this.generateFilesButton.Enabled;
				}
			}
		}

		private sealed class GenerateFilesState
		{
			public object ActiveItem
			{
				get;
				set;
			}

			public bool CreateSubdirectories
			{
				get;
				set;
			}

			public bool CreateVisualStudioProjectFile
			{
				get;
				set;
			}
			
			public string Directory
			{
				get;
				set;
			}
		}
	}
}
