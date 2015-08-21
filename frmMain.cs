using dataport.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace dataport
{
    public partial class frmMain : Form
    {
        DataportServer DataportServer = new DataportServer();

        public frmMain()
        {
            InitializeComponent();

        }

        private void btSwitch_Click(object sender, EventArgs e)
        {
            if (DataportServer.IsRunning)
            {
                DataportServer.Stop();
                btSwitch.Text = "Stopped";
            }
            else
            {
                DataportServer.Port = Convert.ToInt32(txtPort.Text);
                DataportServer.Start();
                btSwitch.Text = "Running";
            }
            

        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(DataportServer.IsRunning) DataportServer.Stop();
        }

        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            int i=0;
            for (;i<s.Length;i++)
            {
                StringBuilder sb = new StringBuilder();
                if (File.Exists(s[i]))
                    sb.Append("");
                else if (Directory.Exists(s[i]))
                    sb.Append("");
                else
                    return;

                if (DataportServer.AddFile(s[i].Substring(s[i].LastIndexOf('\\') + 1), s[i]))
                {
                    sb.Append(s[i].Substring(s[i].LastIndexOf('\\') + 1));
                    listFile.Items.Add(sb.ToString());
                }

            }

        }

        private void listRename_Click(object sender, EventArgs e)
        {
            pRename.Show();
        }

        private void btRename_Click(object sender, EventArgs e)
        {
            if (listFile.SelectedIndex != -1)
            {
                DataportServer.RenameFile(listFile.Items[listFile.SelectedIndex].ToString(), txtRename.Text);
                listFile.Items[listFile.SelectedIndex] = txtRename.Text;
                txtRename.ResetText();
                pRename.Hide();
            }
        }

        private void listDelete_Click(object sender, EventArgs e)
        {
            if (listFile.SelectedIndex != -1 && DataportServer.RemoveFile(listFile.SelectedItem.ToString()))
            {
                listFile.Items.Remove(listFile.SelectedItem);
            }
        }

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                listFile.SelectedIndex = listFile.IndexFromPoint(e.Location);
                if (listFile.SelectedIndex != -1)
                    listMenu.Show(Cursor.Position);
            }
        }

        delegate void AddLogCallback(string log);
        public void AddLog(string log)
        {
            if (listLog.InvokeRequired)
            {
                AddLogCallback dd = new AddLogCallback(AddLog);
                this.BeginInvoke(dd, new object[] { log });
            }
            else
            {
                listLog.Items.Add(log);
            }
        }
        
    }
}
