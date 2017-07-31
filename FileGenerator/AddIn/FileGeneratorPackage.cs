using Reflector;
using Reflector.CodeModel;
using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using FileGenerator.AddIn.UI;

namespace FileGenerator.AddIn
{
	public class FileGeneratorPackage : IPackage
	{
		private const Keys MenuKeys = Keys.Control | Keys.Shift | Keys.G;
		private const string ToolsCommandBar = "Tools";
		private const string ToolsTitle = "&Generate File(s)...";
		private const string WindowID = "FileGeneratorWindow";
		private const string WindowTitle = "Generate Files";

		private ICommandBarButton fileGeneratorCommandBarButton;
		private ICommandBarSeparator fileGeneratorCommandBarSeparator;
		private IWindow fileGeneratorWindow;
		private IServiceProvider serviceProvider;
		private IWindowManager windowManager;

		public FileGeneratorPackage()
			: base()
		{
		}

		public void Load(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider;

			var fileGeneratorControl = new FileGeneratorControl(this.serviceProvider);

			this.windowManager =
				 (IWindowManager)this.serviceProvider.GetService(typeof(IWindowManager));
			this.windowManager.Windows.Add(WindowID, fileGeneratorControl, WindowTitle);

			this.fileGeneratorWindow = this.windowManager.Windows[WindowID];
			this.fileGeneratorWindow.Content.Dock = DockStyle.Fill;

			var commandBarManager =
				 (ICommandBarManager)this.serviceProvider.GetService(typeof(ICommandBarManager));
			var commandToolItems = commandBarManager.CommandBars[ToolsCommandBar].Items;
			this.fileGeneratorCommandBarSeparator = commandToolItems.AddSeparator();
			this.fileGeneratorCommandBarButton = commandToolItems.AddButton(
				 ToolsTitle, new EventHandler(this.OnFileGeneratorButtonClick), MenuKeys);
		}

		private void OnFileGeneratorButtonClick(object sender, EventArgs e)
		{
			this.fileGeneratorWindow.Visible = true;
		}

		public void Unload()
		{
			var commandBarManager =
				(ICommandBarManager)this.serviceProvider.GetService(typeof(ICommandBarManager));
			var commandToolItems = commandBarManager.CommandBars[ToolsCommandBar].Items;

			commandToolItems.Remove(this.fileGeneratorCommandBarButton);
			commandToolItems.Remove(this.fileGeneratorCommandBarSeparator);
		}
	}
}