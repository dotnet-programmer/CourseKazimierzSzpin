using System.ComponentModel;

namespace SendReport.ServiceApp
{
	[RunInstaller(true)]
	public partial class ProjectInstaller : System.Configuration.Install.Installer
	{
		public ProjectInstaller() => InitializeComponent();
	}
}