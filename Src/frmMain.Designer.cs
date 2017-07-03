namespace ScriptTool
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.niScriptTool = new System.Windows.Forms.NotifyIcon(this.components);
            this.mnuIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lstScripts = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuScripts = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuScriptsSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuScriptsAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuScriptsRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuScriptRename = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuScriptsRun = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWindowScripts = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuIconToggleWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuIconSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuIconSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuIconExit = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // niScriptTool
            // 
            this.niScriptTool.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.niScriptTool.BalloonTipText = "Show or Hide the Script Tool or select a script to run it.";
            this.niScriptTool.BalloonTipTitle = "Script Tool";
            this.niScriptTool.ContextMenuStrip = this.mnuIcon;
            this.niScriptTool.Icon = ((System.Drawing.Icon)(resources.GetObject("niScriptTool.Icon")));
            this.niScriptTool.Text = "Script Tool";
            this.niScriptTool.Visible = true;
            this.niScriptTool.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // mnuIcon
            // 
            this.mnuIcon.Name = "mnuIcon";
            this.mnuIcon.Size = new System.Drawing.Size(61, 4);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lstScripts);
            this.splitContainer1.Size = new System.Drawing.Size(784, 537);
            this.splitContainer1.SplitterDistance = 204;
            this.splitContainer1.TabIndex = 0;
            // 
            // lstScripts
            // 
            this.lstScripts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lstScripts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstScripts.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstScripts.DisplayMember = "Name";
            this.lstScripts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstScripts.ForeColor = System.Drawing.Color.White;
            this.lstScripts.FormattingEnabled = true;
            this.lstScripts.Location = new System.Drawing.Point(0, 0);
            this.lstScripts.Name = "lstScripts";
            this.lstScripts.Size = new System.Drawing.Size(204, 537);
            this.lstScripts.Sorted = true;
            this.lstScripts.TabIndex = 3;
            this.lstScripts.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstScripts_MouseClick);
            this.lstScripts.SelectedIndexChanged += new System.EventHandler(this.lstScripts_SelectedIndexChanged);
            this.lstScripts.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstScripts_KeyDown);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuScripts,
            this.mnuWindow,
            this.mnuAbout});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuScripts
            // 
            this.mnuScripts.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuScriptsSave,
            this.toolStripSeparator3,
            this.mnuScriptsAdd,
            this.mnuScriptsRemove,
            this.toolStripSeparator2,
            this.mnuScriptRename,
            this.toolStripSeparator1,
            this.mnuScriptsRun});
            this.mnuScripts.Name = "mnuScripts";
            this.mnuScripts.Size = new System.Drawing.Size(54, 20);
            this.mnuScripts.Text = "&Scripts";
            // 
            // mnuScriptsSave
            // 
            this.mnuScriptsSave.Name = "mnuScriptsSave";
            this.mnuScriptsSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuScriptsSave.Size = new System.Drawing.Size(138, 22);
            this.mnuScriptsSave.Text = "&Save";
            this.mnuScriptsSave.Click += new System.EventHandler(this.mnuScriptsSave_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(135, 6);
            // 
            // mnuScriptsAdd
            // 
            this.mnuScriptsAdd.Name = "mnuScriptsAdd";
            this.mnuScriptsAdd.Size = new System.Drawing.Size(138, 22);
            this.mnuScriptsAdd.Text = "&Add";
            this.mnuScriptsAdd.Click += new System.EventHandler(this.mnuScriptsAdd_Click);
            // 
            // mnuScriptsRemove
            // 
            this.mnuScriptsRemove.Name = "mnuScriptsRemove";
            this.mnuScriptsRemove.Size = new System.Drawing.Size(138, 22);
            this.mnuScriptsRemove.Text = "&Remove";
            this.mnuScriptsRemove.Click += new System.EventHandler(this.mnuScriptsRemove_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(135, 6);
            // 
            // mnuScriptRename
            // 
            this.mnuScriptRename.Name = "mnuScriptRename";
            this.mnuScriptRename.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.mnuScriptRename.Size = new System.Drawing.Size(138, 22);
            this.mnuScriptRename.Text = "R&ename";
            this.mnuScriptRename.Click += new System.EventHandler(this.mnuScriptRename_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(135, 6);
            // 
            // mnuScriptsRun
            // 
            this.mnuScriptsRun.Enabled = false;
            this.mnuScriptsRun.Name = "mnuScriptsRun";
            this.mnuScriptsRun.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.mnuScriptsRun.Size = new System.Drawing.Size(138, 22);
            this.mnuScriptsRun.Text = "R&un";
            this.mnuScriptsRun.Click += new System.EventHandler(this.mnuScriptsRun_Click);
            // 
            // mnuWindow
            // 
            this.mnuWindow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuWindowScripts});
            this.mnuWindow.Name = "mnuWindow";
            this.mnuWindow.Size = new System.Drawing.Size(63, 20);
            this.mnuWindow.Text = "Window";
            // 
            // mnuWindowScripts
            // 
            this.mnuWindowScripts.Name = "mnuWindowScripts";
            this.mnuWindowScripts.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.mnuWindowScripts.Size = new System.Drawing.Size(165, 22);
            this.mnuWindowScripts.Text = "&Switch Panels";
            this.mnuWindowScripts.Click += new System.EventHandler(this.mnuWindowScripts_Click);
            // 
            // mnuAbout
            // 
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(52, 20);
            this.mnuAbout.Text = "&About";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // mnuIconToggleWindow
            // 
            this.mnuIconToggleWindow.Name = "mnuIconToggleWindow";
            this.mnuIconToggleWindow.Size = new System.Drawing.Size(32, 19);
            // 
            // mnuIconSeparator2
            // 
            this.mnuIconSeparator2.Name = "mnuIconSeparator2";
            this.mnuIconSeparator2.Size = new System.Drawing.Size(6, 6);
            // 
            // mnuIconSeparator1
            // 
            this.mnuIconSeparator1.Name = "mnuIconSeparator1";
            this.mnuIconSeparator1.Size = new System.Drawing.Size(6, 6);
            // 
            // mnuIconExit
            // 
            this.mnuIconExit.Name = "mnuIconExit";
            this.mnuIconExit.Size = new System.Drawing.Size(32, 19);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "Script Tool";
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon niScriptTool;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox lstScripts;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuScripts;
        private System.Windows.Forms.ToolStripMenuItem mnuScriptsAdd;
        private System.Windows.Forms.ToolStripMenuItem mnuScriptsRemove;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ContextMenuStrip mnuIcon;
        private System.Windows.Forms.ToolStripMenuItem mnuIconToggleWindow;
        private System.Windows.Forms.ToolStripSeparator mnuIconSeparator2;
        private System.Windows.Forms.ToolStripSeparator mnuIconSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuIconExit;
        private System.Windows.Forms.ToolStripMenuItem mnuScriptRename;
        private System.Windows.Forms.ToolStripMenuItem mnuWindow;
        private System.Windows.Forms.ToolStripMenuItem mnuWindowScripts;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.ToolStripMenuItem mnuScriptsSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuScriptsRun;
    }
}

