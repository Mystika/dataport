namespace dataport
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
            this.btSwitch = new System.Windows.Forms.Button();
            this.listFile = new System.Windows.Forms.ListBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.listMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.listRename = new System.Windows.Forms.ToolStripMenuItem();
            this.listDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.txtRename = new System.Windows.Forms.TextBox();
            this.btRename = new System.Windows.Forms.Button();
            this.pRename = new System.Windows.Forms.Panel();
            this.listLog = new System.Windows.Forms.ListBox();
            this.listMenu.SuspendLayout();
            this.pRename.SuspendLayout();
            this.SuspendLayout();
            // 
            // btSwitch
            // 
            this.btSwitch.Location = new System.Drawing.Point(0, 0);
            this.btSwitch.Name = "btSwitch";
            this.btSwitch.Size = new System.Drawing.Size(80, 35);
            this.btSwitch.TabIndex = 0;
            this.btSwitch.Text = "Stopped";
            this.btSwitch.UseVisualStyleBackColor = true;
            this.btSwitch.Click += new System.EventHandler(this.btSwitch_Click);
            // 
            // listFile
            // 
            this.listFile.AllowDrop = true;
            this.listFile.FormattingEnabled = true;
            this.listFile.ItemHeight = 12;
            this.listFile.Location = new System.Drawing.Point(0, 35);
            this.listFile.Name = "listFile";
            this.listFile.Size = new System.Drawing.Size(524, 328);
            this.listFile.TabIndex = 1;
            this.listFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.listBox1_DragDrop);
            this.listFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.listBox1_DragEnter);
            this.listFile.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseDown);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(86, 8);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(114, 21);
            this.txtPort.TabIndex = 2;
            this.txtPort.Text = "8080";
            // 
            // listMenu
            // 
            this.listMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.listRename,
            this.listDelete});
            this.listMenu.Name = "listMenu";
            this.listMenu.Size = new System.Drawing.Size(118, 48);
            // 
            // listRename
            // 
            this.listRename.Name = "listRename";
            this.listRename.Size = new System.Drawing.Size(117, 22);
            this.listRename.Text = "Rename";
            this.listRename.Click += new System.EventHandler(this.listRename_Click);
            // 
            // listDelete
            // 
            this.listDelete.Name = "listDelete";
            this.listDelete.Size = new System.Drawing.Size(117, 22);
            this.listDelete.Text = "Delete";
            this.listDelete.Click += new System.EventHandler(this.listDelete_Click);
            // 
            // txtRename
            // 
            this.txtRename.Location = new System.Drawing.Point(3, 13);
            this.txtRename.Name = "txtRename";
            this.txtRename.Size = new System.Drawing.Size(626, 21);
            this.txtRename.TabIndex = 4;
            // 
            // btRename
            // 
            this.btRename.Location = new System.Drawing.Point(635, 13);
            this.btRename.Name = "btRename";
            this.btRename.Size = new System.Drawing.Size(70, 19);
            this.btRename.TabIndex = 5;
            this.btRename.Text = "Rename";
            this.btRename.UseVisualStyleBackColor = true;
            this.btRename.Click += new System.EventHandler(this.btRename_Click);
            // 
            // pRename
            // 
            this.pRename.Controls.Add(this.txtRename);
            this.pRename.Controls.Add(this.btRename);
            this.pRename.Location = new System.Drawing.Point(0, 362);
            this.pRename.Name = "pRename";
            this.pRename.Size = new System.Drawing.Size(715, 50);
            this.pRename.TabIndex = 6;
            this.pRename.Visible = false;
            // 
            // listLog
            // 
            this.listLog.AllowDrop = true;
            this.listLog.FormattingEnabled = true;
            this.listLog.HorizontalScrollbar = true;
            this.listLog.ItemHeight = 12;
            this.listLog.Location = new System.Drawing.Point(530, 35);
            this.listLog.Name = "listLog";
            this.listLog.Size = new System.Drawing.Size(185, 328);
            this.listLog.TabIndex = 7;
            // 
            // frmMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 412);
            this.Controls.Add(this.listLog);
            this.Controls.Add(this.pRename);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.listFile);
            this.Controls.Add(this.btSwitch);
            this.Name = "frmMain";
            this.Text = "Fileport";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.listMenu.ResumeLayout(false);
            this.pRename.ResumeLayout(false);
            this.pRename.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btSwitch;
        private System.Windows.Forms.ListBox listFile;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.ContextMenuStrip listMenu;
        private System.Windows.Forms.ToolStripMenuItem listRename;
        private System.Windows.Forms.ToolStripMenuItem listDelete;
        private System.Windows.Forms.TextBox txtRename;
        private System.Windows.Forms.Button btRename;
        private System.Windows.Forms.Panel pRename;
        private System.Windows.Forms.ListBox listLog;
    }
}

