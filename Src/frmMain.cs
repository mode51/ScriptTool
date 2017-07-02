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
        public class Settings
        {
            public List<ScriptItem> Scripts { get; set; } = new List<ScriptItem>();
        }
        private Settings _settings = new Settings();
        private bool _forceClose = false;
        private ScintillaNET.Scintilla _editor;

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
            }
            base.OnClosing(e);
        }

        #region NotifyIcon methods
        private void BuildIconMenu()
        {
            mnuIcon.Items.Clear();
            foreach (var script in _settings.Scripts.OrderByDescending(item => item.Name))
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

        private string EditorScript
        {
            get
            {
                return _editor.Text;
                //return SyntaxEditor.Document.CurrentSnapshot.GetText();
            }
            set
            {
                _editor.Text = value;
                //if (SyntaxEditor.Document == null)
                //{
                //    TextDocument document = new TextDocument();
                //    //document.InitializeText(value);
                //    document.Language = new CSharpLanguage();
                //    SyntaxEditor.Document = document;
                //}
                //SyntaxEditor.Document.InitializeText(value);
                ////SyntaxEditor.Document = document;
            }
        }
        private void ToggleVisibility()
        {
            this.Visible = !this.Visible;
        }
        private void RunCurrentScript()
        {
            if (CurrentScript == null)
                return;
            CurrentScript.Script = EditorScript;
            SaveSettings();
            RunScript(CurrentScript);
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
                    Task.Run(() => { cs.Invoke(); });
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
        private void ShowScripts()
        {
            int index = -1;
            index = lstScripts.SelectedIndex;
            lstScripts.BeginUpdate();
            lstScripts.Items.Clear();
            foreach (var item in _settings.Scripts)
            {
                lstScripts.Items.Add(item);
            }
            lstScripts.SelectedIndex = index;
            lstScripts.EndUpdate();
        }
        private void AddScript(string name, string script, bool select)
        {
            var newScript = new ScriptItem();
            newScript.Name = name;
            newScript.Script = script;
            _settings.Scripts.Add(newScript);

            var newIndex = lstScripts.Items.Add(newScript);
            if (select)
                lstScripts.SelectedIndex = newIndex;
            BuildIconMenu();
        }
        private string GetSettingsFilename()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filename = System.IO.Path.Combine(path, "ScriptTool.json");
            return filename;
        }
        private void SaveSettings()
        {
            //await Task.Run(() =>
            //{
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
            //});
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

        private void lstScripts_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtScript.Text = CurrentScript?.Script;
            //EditorScript = CurrentScript?.Script ?? "";
            EditorScript = CurrentScript?.Script ?? "";
            mnuScriptsRun.Enabled = CurrentScript != null;
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
            var scriptName = Microsoft.VisualBasic.Interaction.InputBox("Please enter a name for this script", "New Script");
            if (string.IsNullOrEmpty(scriptName))
                return;
            var methodName = scriptName.Replace(' ', '_');
            var script = string.Format("// The first static method is the script entry point.\n// You can add usings statements and additional static methods.\n// You cannot add namespaces and classes.\n\nusing System.Windows.Forms;\n\npublic static void {0}()\n{{\n\tMessageBox.Show(\"Hello World\", \"Script Tool\");\n\n}}\n", methodName);
            AddScript(scriptName, script, true);
            SaveSettings();
        }
        private void mnuScriptsRemove_Click(object sender, EventArgs e)
        {
            for (int t = lstScripts.SelectedIndices.Count - 1; t >= 0; t--)
            {
                var index = lstScripts.SelectedIndices[t];
                var script = (ScriptItem)lstScripts.Items[index];

                lstScripts.Items.RemoveAt(index);
                _settings.Scripts.Remove(script);
            }
        }
        private void mnuScriptRename_Click(object sender, EventArgs e)
        {
            if (lstScripts.SelectedIndex < 0)
                return;
            var newName = Microsoft.VisualBasic.Interaction.InputBox("Enter a new name for this script", "Script Tool", CurrentScript.Name);
            if (!string.IsNullOrEmpty(newName))
            {
                var selectedIndex = lstScripts.SelectedIndex;
                CurrentScript.Name = newName;
                ShowScripts();
                lstScripts.SelectedIndex = selectedIndex;
            }
        }
        private void mnuScriptsRun_Click(object sender, EventArgs e)
        {
            RunCurrentScript();
        }
        private void mnuWindowScripts_Click(object sender, EventArgs e)
        {
            if (this.splitContainer1.ActiveControl == lstScripts)
                _editor.Select();
            else
                lstScripts.Select();
        }
        private void mnuHelpAbout_Click(object sender, EventArgs e)
        {
            using (var dlg = new frmAbout())
                dlg.ShowDialog();
        }
    }
}
