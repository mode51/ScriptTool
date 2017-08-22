# ScriptTool

To load the Installer project you will need to have previously installed [Microsoft Visual Studio 2017 Installer Projects](https://marketplace.visualstudio.com/items?itemName=VisualStudioProductTeam.MicrosoftVisualStudio2017InstallerProjects).

Scripts are run in a background thread to allow for more complex scripts.  They are also tracked and terminated forcefully if still running when the application closes.  To terminate the application, you must right-click the system tray icon and select &lt;Exit&gt; because closing the window only hides it.

#### Thanks
A mighty thanks to these two guys. Without these two projects, ScriptTool wouldn't exist.
* Oleg Shilo for [CS-Script](https://github.com/oleg-shilo/cs-script).
* jacobslusser for [ScintillaNET](https://github.com/jacobslusser/ScintillaNET).

___
![alt text](https://github.com/mode51/ScriptTool/blob/master/Src/ScriptTool.png)

