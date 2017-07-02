using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration.Install;
using System.ComponentModel;
using System.Reflection;
using System.Diagnostics;
using System.IO;

namespace ScriptTool
{
    //See https://www.codeproject.com/Articles/19560/Launching-Your-Application-After-Install-using-Vis

    // Taken from:http://msdn2.microsoft.com/en-us/library/
    // system.configuration.configurationmanager.aspx
    // Set 'RunInstaller' attribute to true.

    [RunInstaller(true)]
    public class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
          : base()
        {
            // Attach the 'Committed' event.
            this.Committed += new InstallEventHandler(OnCommitted);
            // Attach the 'Committing' event.
            this.Committing += new InstallEventHandler(OnCommitting);
        }

        // Event handler for 'Committing' event.
        private void OnCommitting(object sender, InstallEventArgs e)
        {
            //Console.WriteLine("");
            //Console.WriteLine("Committing Event occurred.");
            //Console.WriteLine("");
        }

        // Event handler for 'Committed' event.
        private void OnCommitted(object sender, InstallEventArgs e)
        {
            try
            {
                Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                Process.Start(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\ScriptTool.exe");
            }
            catch
            {
                // Do nothing... 
            }
        }

        // Override the 'Install' method.
        public override void Install(IDictionary savedState)
        {
            base.Install(savedState);
        }

        // Override the 'Commit' method.
        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);
        }

        // Override the 'Rollback' method.
        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);
        }
    }
}
