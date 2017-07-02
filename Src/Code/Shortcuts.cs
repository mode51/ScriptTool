using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace ScriptTool
{
    public class Shortcuts
    {
        //Sample Use:
        //TheGeneral.Common.IO.Shortcuts.Win32.CreateShortcut(startupDirectory, "Simple Folder Sync", executablePath, "Simple Folder Sync", "", workingDirectory, executablePath);
        public static IShellLink CreateInStartupFolder(string name, string description, string arguments = "")
        {
            string codeBase = System.Reflection.Assembly.GetEntryAssembly().GetName().CodeBase;
            string executablePath = new Uri(codeBase).LocalPath;
            string currentDirectory = System.IO.Path.GetDirectoryName(executablePath);
            string startupDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Startup);

            return Create(startupDirectory, name, executablePath, description, arguments, currentDirectory, executablePath);
        }
        public static IShellLink Create(string folderPath, string name, string target, string description = "", string arguments = "", string workingDirectory = "", string iconPath = "", int iconIndex = -1)
        {
            var shortcut = new ShellLink() as IShellLink;
            if (shortcut != null && !string.IsNullOrEmpty(target) && !string.IsNullOrEmpty(name))
            {
                // setup shortcut information
                shortcut.SetPath(target);
                if (!string.IsNullOrEmpty(description))
                    shortcut.SetDescription(description);
                if (!string.IsNullOrEmpty(iconPath) && iconIndex >= 0)
                    shortcut.SetIconLocation(iconPath, iconIndex);
                if (!string.IsNullOrEmpty(arguments))
                    shortcut.SetArguments(arguments);
                if (!string.IsNullOrEmpty(workingDirectory))
                    shortcut.SetWorkingDirectory(workingDirectory);

                // save it
                var file = (IPersistFile)shortcut;
                if (!name.EndsWith(".lnk"))
                    name += ".lnk";
                file.Save(Path.Combine(folderPath, name), false);
            }
            return shortcut;
        }

        [ComImport]
        [Guid("00021401-0000-0000-C000-000000000046")]
        internal class ShellLink
        {
        }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("000214F9-0000-0000-C000-000000000046")]
        public interface IShellLink
        {
            void GetPath([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile, int cchMaxPath, out IntPtr pfd, int fFlags);
            void GetIDList(out IntPtr ppidl);
            void SetIDList(IntPtr pidl);
            void GetDescription([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszName, int cchMaxName);
            void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);
            void GetWorkingDirectory([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir, int cchMaxPath);
            void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);
            void GetArguments([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs, int cchMaxPath);
            void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);
            void GetHotkey(out short pwHotkey);
            void SetHotkey(short wHotkey);
            void GetShowCmd(out int piShowCmd);
            void SetShowCmd(int iShowCmd);
            void GetIconLocation([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath, int cchIconPath, out int piIcon);
            void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);
            void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, int dwReserved);
            void Resolve(IntPtr hwnd, int fFlags);
            void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
        }
    }
}
