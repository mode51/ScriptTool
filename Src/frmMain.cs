using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using W;
using W.Logging;

namespace ScriptTool
{
    public partial class frmMain : Form
    {
        public class ScriptItem
        {
            public string Name { get; set; }
            public string Script { get; set; }
        }
        public class Version1Settings
        {
            public List<ScriptItem> Scripts { get; set; } = new List<ScriptItem>();
        }
        public class Settings
        {
            public Version1Settings V1 { get; set; } = new Version1Settings();
        }
        private class ScriptThread
        {
            public ScriptItem Script { get; set; }
            public W.Threading.Thread Thread { get; set; }
            public System.Threading.CancellationTokenSource Cts { get; set; } = new System.Threading.CancellationTokenSource();
        }
        private Settings _settings = new Settings();
        private bool _forceClose = false;
        private ScintillaNET.Scintilla _editor;
        private List<ScriptThread> _threads = new List<ScriptThread>();

        private ScriptItem CurrentScript
        {
            get
            {
                var script = lstScripts.SelectedItem as ScriptItem;
                return script;
            }
        }
        public frmMain()
        {
            InitializeComponent();
            Log.LogTheMessage = (category, message) =>
            {
                System.Diagnostics.Debug.WriteLine("{0}: {1}", category, message);
            };
            Log.i("ScriptTool Initailized");

            Task.Run(() =>
            {
                LoadSettings();
                this.InvokeEx(f =>
                {
                    ShowScripts();
                });
                BuildIconMenu();
            });

            _editor = new ScintillaNET.Scintilla();
            _editor.BorderStyle = BorderStyle.None;
            _editor.Dock = DockStyle.Fill;
            _editor.Enabled = false;
            splitContainer1.Panel2.Controls.Add(_editor);
            _editor.CaretForeColor = System.Drawing.Color.White;
            Helpers.InitHotkeys(this, _editor);
            Helpers.InitSyntaxColoring(_editor);
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            if (!_forceClose)
            {
                Hide();
                e.Cancel = true;
            }
            else
            {
                SaveSettings();
                TerminateAllThreads();
            }
            base.OnClosing(e);
        }

        #region NotifyIcon methods
        private void BuildIconMenu()
        {
            mnuIcon.Items.Clear();
            foreach (var script in _settings.V1.Scripts.OrderByDescending(item => item.Name))
            {
                mnuIcon.Items.Add(script.Name, null, (o, e) => { RunScript(script); });
            }
            mnuIcon.Items.Add(new ToolStripSeparator());
            var mnuIconToggle = mnuIcon.Items.Add("Show/Hide Script Tool Window", null, (o, e) => { ToggleVisibility(); });
            mnuIconToggle.Font = new Font(mnuIconToggle.Font, FontStyle.Bold);
            mnuIcon.Items.Add("E&xit", null, (o, e) => { _forceClose = true; Close(); });
        }
        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            ToggleVisibility();
        }
        #endregion

        #region Private Methods
        private void TerminateAllThreads()
        {
            //if all threads exited propertyly, there shouldn't be any threads to close here
            while(_threads.Count > 0)
            {
                var thread = _threads[0];
                try
                {
                    thread.Cts.Cancel();
                    if (!thread.Thread.Join(1000))
                        thread.Thread.Cancel(4000);
                }
                catch { }
            }
            _threads.Clear();
        }
        private string EditorScript
        {
            get
            {
                return _editor.Text;
            }
            set
            {
                _editor.Text = value;
            }
        }
        private void ToggleVisibility()
        {
            this.Visible = !this.Visible;
        }
        private void TogglePanelSelection()
        {
            if (this.splitContainer1.ActiveControl == lstScripts)
                _editor.Select();
            else
                lstScripts.Select();
        }
        private void ShowScripts()
        {
            var selectedItem = lstScripts.SelectedItem;
            lstScripts.BeginUpdate();
            lstScripts.Items.Clear();
            foreach (var item in _settings.V1.Scripts)
            {
                lstScripts.Items.Add(item);
            }
            lstScripts.SelectedItem = selectedItem;
            lstScripts.EndUpdate();
        }
        private void AddScript()
        {
            var scriptName = Microsoft.VisualBasic.Interaction.InputBox("Please enter a name for this script", "New Script");
            if (string.IsNullOrEmpty(scriptName))
                return;
            var methodName = scriptName.Replace(' ', '_');
            var script = string.Format("// The first static method is the script entry point.\n// You may add usings statements, additional static methods and classes.\n// You may not add namespaces.\n\nusing System.Windows.Forms;\n\npublic static void {0}()\n{{\n\tMessageBox.Show(\"Hello World\", \"Script Tool\");\n}}\n", methodName);

            var newScript = new ScriptItem();
            newScript.Name = scriptName;
            newScript.Script = script;
            _settings.V1.Scripts.Add(newScript);

            lstScripts.Items.Add(newScript);
            lstScripts.SelectedItem = newScript;
            _editor.Select();
            BuildIconMenu();
        }
        private void RunScript(ScriptItem script)
        {
            //run the current script
            if (script != null)
            {
                try
                {
                    //var action = CSScriptLibrary.CSScript.CreateAction(script.Script);
                    //action.Invoke();

                    var asm = CSScriptLibrary.CSScript.LoadMethod(script.Script);
                    var cs = asm.GetStaticMethod();// "Main");
                    var sc = new ScriptThread() { Script = script };
                    sc.Thread = W.Threading.Thread.Create(cts =>
                    {
                        try
                        {
                            cs.Invoke();
                        }
                        catch { }
                    }, (r, e) =>
                    {
                        try
                        {
                            _threads.Remove(sc);
                        }
                        catch { }
                    }, sc.Cts);
                    _threads.Add(sc);
                    //Task.Run(() => { cs.Invoke(); });
                    //cs.ShowMessage();
                }
                catch (csscript.CompilerException e)
                {
                    string msg = string.Empty;
                    var index = e.Message.IndexOf(" error ");
                    if (index >= 0)
                        msg += e.Message.Substring(index + 1);
                    else
                        msg += e.Message;
                    MessageBox.Show(e.ToString(), "Script Tool");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), "Script Tool");
                }
            }
        }
        private void RunCurrentScript()
        {
            if (CurrentScript == null)
                return;
            CurrentScript.Script = EditorScript;
            SaveSettings();
            RunScript(CurrentScript);
        }
        private void RenameCurrentScript()
        {
            if (lstScripts.SelectedItem == null)
                return;
            var newName = Microsoft.VisualBasic.Interaction.InputBox("Enter a new name for this script", "Script Tool", CurrentScript.Name);
            if (!string.IsNullOrEmpty(newName))
            {
                var selectedItem = lstScripts.SelectedItem;
                CurrentScript.Name = newName;
                ShowScripts();
                lstScripts.SelectedItem = selectedItem;
            }
        }
        private void RemoveCurrentScript()
        {
            var selectedIndex = lstScripts.SelectedIndex;
            for (int t = lstScripts.SelectedIndices.Count - 1; t >= 0; t--)
            {
                var index = lstScripts.SelectedIndices[t];
                var script = (ScriptItem)lstScripts.Items[index];

                lstScripts.Items.RemoveAt(index);
                _settings.V1.Scripts.Remove(script);
            }
            if (lstScripts.Items.Count > 0)
                lstScripts.SelectedIndex = Math.Max(selectedIndex - 1, 0);
            else
                lstScripts.ClearSelected();
        }
        private string GetSettingsFilename()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filename = System.IO.Path.Combine(path, "ScriptTool.json");
            return filename;
        }
        private void SaveSettings()
        {
            var filename = GetSettingsFilename();
            try
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(_settings);
                System.IO.File.WriteAllText(GetSettingsFilename(), json);
            }
            catch (Exception e)
            {
                this.InvokeEx(f =>
                {
                    MessageBox.Show(e.ToString(), "Script Tool", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                });
            }
        }
        private void LoadSettings()
        {
            var filename = GetSettingsFilename();
            if (!System.IO.File.Exists(filename))
                return;
            try
            {
                var json = System.IO.File.ReadAllText(filename);
                Newtonsoft.Json.JsonConvert.PopulateObject(json, _settings);
            }
            catch (Exception e)
            {
                this.InvokeEx(f =>
                {
                    MessageBox.Show(e.ToString(), "Script Tool", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                });
            }
        }
        #endregion

        #region Menu Handlers
        private void lstScripts_SelectedIndexChanged(object sender, EventArgs e)
        {
            EditorScript = CurrentScript?.Script ?? "";
            mnuScriptsRun.Enabled = CurrentScript != null;
            _editor.Enabled = lstScripts.SelectedItem != null;
        }
        private void lstScripts_MouseClick(object sender, MouseEventArgs e)
        {
            if (lstScripts.IndexFromPoint(e.Location) < 0)
                lstScripts.ClearSelected();
        }
        private void lstScripts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                lstScripts.ClearSelected();
        }

        private void mnuScriptsSave_Click(object sender, EventArgs e)
        {
            if (CurrentScript != null)
                CurrentScript.Script = EditorScript;// txtScript.Text;
            SaveSettings();
        }
        private void mnuScriptsAdd_Click(object sender, EventArgs e)
        {
            AddScript();
            SaveSettings();
        }
        private void mnuScriptsRemove_Click(object sender, EventArgs e)
        {
            RemoveCurrentScript();
            BuildIconMenu();
        }
        private void mnuScriptRename_Click(object sender, EventArgs e)
        {
            RenameCurrentScript();
        }
        private void mnuScriptsRun_Click(object sender, EventArgs e)
        {
            RunCurrentScript();
        }
        private void mnuWindowScripts_Click(object sender, EventArgs e)
        {
            TogglePanelSelection();
        }
        private void mnuAbout_Click(object sender, EventArgs e)
        {
            using (var dlg = new frmAbout())
                dlg.ShowDialog();
        }
        #endregion
    }
}
