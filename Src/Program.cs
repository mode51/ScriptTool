using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using W;
using W.Logging;

namespace ScriptTool
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppDomain.CurrentDomain.FirstChanceException += (o, e) =>
            {
                Log.e(e.Exception);
            };
            AppDomain.CurrentDomain.UnhandledException += (o, e) =>
            {
                Log.e((Exception)e.ExceptionObject);
            };

            Log.i("Script Tool Starting...");
            if (!SingleInstance.Start())
            {
                // Another instance is already started.
                Log.w("Script Tool exiting...another instance is already running.");
                return;
            }
            Log.i("Script Tool checking start with windows configuration");
            CheckAutostart();

            try
            {
                Application.Run(new frmMain());
            }
            catch (Exception e)
            {
                Log.e(e);
            }
            
            SingleInstance.Stop();
        }
        private static void CheckAutostart()
        {
            string codeBase = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
            string executablePath = new Uri(codeBase).LocalPath;
            string currentDirectory = System.IO.Path.GetDirectoryName(executablePath);
            string startupDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Startup);

            //create shortcut to executable
            Shortcuts.Create(startupDirectory, "Script Tool", executablePath, "Script Tool", "", currentDirectory, executablePath);

            //// Copy self to Startup folder if not started from there.
            //if (currentDirectory != startupDirectory)
            //{                
            //    string executableName = System.IO.Path.GetFileName(executablePath);
            //    System.IO.File.Copy(executablePath, startupDirectory + "/" + executableName, true);
            //}
            Log.i("Script Tool added to Startup folder.");
        }
    }
}
