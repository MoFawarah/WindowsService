using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace MyWindowsService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();

            // ServiceProcessInstaller
            ServiceProcessInstaller serviceProcessInstaller = new ServiceProcessInstaller
            {
                Account = ServiceAccount.LocalSystem // You can change this to other accounts as needed
            };

            // ServiceInstaller
            ServiceInstaller serviceInstaller = new ServiceInstaller
            {
                StartType = ServiceStartMode.Automatic, // The service will start automatically
                ServiceName = "MyWindowsService", // Must match the service name in your code
                DisplayName = "My Windows Service", // This is what shows up in Services.msc
                Description = "This is a sample Windows Service for demonstration purposes." // Service description
            };

            // Add installers to the Installers collection
            Installers.Add(serviceProcessInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
