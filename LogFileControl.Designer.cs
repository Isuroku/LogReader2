
namespace LogReader2
{
    partial class LogFileControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelCaption = new System.Windows.Forms.Panel();
            this.labelFileName = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.tbLogText = new System.Windows.Forms.TextBox();
            this.panelSplit = new System.Windows.Forms.Panel();
            this.panelCaption.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCaption
            // 
            this.panelCaption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelCaption.Controls.Add(this.btnClose);
            this.panelCaption.Controls.Add(this.labelFileName);
            this.panelCaption.Location = new System.Drawing.Point(3, 3);
            this.panelCaption.Name = "panelCaption";
            this.panelCaption.Size = new System.Drawing.Size(457, 37);
            this.panelCaption.TabIndex = 0;
            // 
            // labelFileName
            // 
            this.labelFileName.AutoSize = true;
            this.labelFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelFileName.Location = new System.Drawing.Point(3, 9);
            this.labelFileName.Name = "labelFileName";
            this.labelFileName.Size = new System.Drawing.Size(51, 20);
            this.labelFileName.TabIndex = 0;
            this.labelFileName.Text = "label1";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnClose.Location = new System.Drawing.Point(418, 9);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(24, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tbLogText
            // 
            this.tbLogText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLogText.Location = new System.Drawing.Point(3, 46);
            this.tbLogText.Multiline = true;
            this.tbLogText.Name = "tbLogText";
            this.tbLogText.Size = new System.Drawing.Size(457, 649);
            this.tbLogText.TabIndex = 1;
            // 
            // panelSplit
            // 
            this.panelSplit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSplit.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.panelSplit.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.panelSplit.Location = new System.Drawing.Point(465, 3);
            this.panelSplit.Name = "panelSplit";
            this.panelSplit.Size = new System.Drawing.Size(10, 692);
            this.panelSplit.TabIndex = 2;
            this.panelSplit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelSplit_MouseDown);
            this.panelSplit.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelSplit_MouseMove);
            this.panelSplit.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelSplit_MouseUp);
            // 
            // LogFileControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelSplit);
            this.Controls.Add(this.tbLogText);
            this.Controls.Add(this.panelCaption);
            this.Name = "LogFileControl";
            this.Size = new System.Drawing.Size(478, 698);
            this.panelCaption.ResumeLayout(false);
            this.panelCaption.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelCaption;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label labelFileName;
        private System.Windows.Forms.TextBox tbLogText;
        private System.Windows.Forms.Panel panelSplit;
    }
}
