
namespace LogReader2
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.openLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openLogWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainerMainLog = new System.Windows.Forms.SplitContainer();
            this.btnLogHide = new System.Windows.Forms.Button();
            this.rtLog = new System.Windows.Forms.RichTextBox();
            this.miOpenHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMainLog)).BeginInit();
            this.splitContainerMainLog.Panel2.SuspendLayout();
            this.splitContainerMainLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openLogToolStripMenuItem,
            this.miOpenHistory,
            this.openLogWindowToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1332, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // openLogToolStripMenuItem
            // 
            this.openLogToolStripMenuItem.Name = "openLogToolStripMenuItem";
            this.openLogToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openLogToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.openLogToolStripMenuItem.Text = "Open Log";
            this.openLogToolStripMenuItem.Click += new System.EventHandler(this.openLogToolStripMenuItem_Click);
            // 
            // openLogWindowToolStripMenuItem
            // 
            this.openLogWindowToolStripMenuItem.Name = "openLogWindowToolStripMenuItem";
            this.openLogWindowToolStripMenuItem.Size = new System.Drawing.Size(118, 20);
            this.openLogWindowToolStripMenuItem.Text = "Open Log Window";
            this.openLogWindowToolStripMenuItem.Click += new System.EventHandler(this.openLogWindowToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // splitContainerMainLog
            // 
            this.splitContainerMainLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerMainLog.Location = new System.Drawing.Point(0, 27);
            this.splitContainerMainLog.Name = "splitContainerMainLog";
            this.splitContainerMainLog.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMainLog.Panel1
            // 
            this.splitContainerMainLog.Panel1.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.splitContainerMainLog_Panel1_ControlAdded);
            this.splitContainerMainLog.Panel1.Resize += new System.EventHandler(this.splitContainerMainLog_Panel1_Resize);
            // 
            // splitContainerMainLog.Panel2
            // 
            this.splitContainerMainLog.Panel2.Controls.Add(this.btnLogHide);
            this.splitContainerMainLog.Panel2.Controls.Add(this.rtLog);
            this.splitContainerMainLog.Size = new System.Drawing.Size(1332, 698);
            this.splitContainerMainLog.SplitterDistance = 585;
            this.splitContainerMainLog.TabIndex = 3;
            this.splitContainerMainLog.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainerMainLog_SplitterMoved);
            // 
            // btnLogHide
            // 
            this.btnLogHide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogHide.Location = new System.Drawing.Point(1288, 3);
            this.btnLogHide.Name = "btnLogHide";
            this.btnLogHide.Size = new System.Drawing.Size(41, 23);
            this.btnLogHide.TabIndex = 1;
            this.btnLogHide.Text = "Hide";
            this.btnLogHide.UseVisualStyleBackColor = true;
            this.btnLogHide.Click += new System.EventHandler(this.btnLogHide_Click);
            // 
            // rtLog
            // 
            this.rtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtLog.Location = new System.Drawing.Point(3, 3);
            this.rtLog.Name = "rtLog";
            this.rtLog.Size = new System.Drawing.Size(1279, 103);
            this.rtLog.TabIndex = 0;
            this.rtLog.Text = "";
            // 
            // miOpenHistory
            // 
            this.miOpenHistory.Name = "miOpenHistory";
            this.miOpenHistory.Size = new System.Drawing.Size(89, 20);
            this.miOpenHistory.Text = "Open History";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1332, 725);
            this.Controls.Add(this.splitContainerMainLog);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.Form1_ControlAdded);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainerMainLog.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMainLog)).EndInit();
            this.splitContainerMainLog.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openLogToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainerMainLog;
        private System.Windows.Forms.RichTextBox rtLog;
        private System.Windows.Forms.Button btnLogHide;
        private System.Windows.Forms.ToolStripMenuItem openLogWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miOpenHistory;
    }
}

