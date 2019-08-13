using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TaskKiller
{
    public partial class Form1 : Form
    {
        int delay = 1000;
        string taskname = "";

        public Form1()
        {
            InitializeComponent();

            init();
        }

        private void init()
        {
 
            string delayStr = ConfigurationManager.AppSettings["delay"];
            try
            {
                delay = (int)(Double.Parse(delayStr) * 1000);
                if (delay < 1000)
                    delay = 1000;
            }
            catch
            {
                delay = 1000;
            }
            

            taskname = ConfigurationManager.AppSettings["taskname"];
            
            Thread t = new Thread(Taskkill);
            t.IsBackground = true;
            t.Start();

            HotKey.RegisterHotKey(Handle, 100, HotKey.KeyModifiers.None, Keys.F1);
            HotKey.RegisterHotKey(Handle, 101, HotKey.KeyModifiers.None, Keys.F2);
            //HotKey.UnregisterHotKey(Handle, 100);
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    switch (m.WParam.ToInt32())
                    {
                        case 100:
                            runTask(taskname);
                            break;
                        case 101:
                            Taskkill(taskname);
                            break;
                        case 102:
                            //MessageBox.Show("102");
                            break;
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.F))
            {
                MessageBox.Show("What the Ctrl+F?");
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void Taskkill()
        {
            Thread.Sleep(delay);
            Taskkill(taskname);
            //Thread.Sleep(delay);
            //runTask("explorer.exe");
        }

        private void runTask(string ProcessName)
        {
            try
            {
                System.Diagnostics.Process.Start(ProcessName);
            }
            catch
            {
                
            }
        }

        private void Taskkill(string ProcessName)
        {
            try
            {
                using (Process P = new Process())
                {
                    P.StartInfo = new ProcessStartInfo()
                    {
                        FileName = "taskkill",
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        Arguments = "/F /IM \"" + ProcessName + "\""
                    };
                    P.Start();
                    P.WaitForExit(60000);
                }
            }
            catch
            {
                /*using (Process P = new Process())
                {
                    P.StartInfo = new ProcessStartInfo()
                    {
                        FileName = "tskill",
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        Arguments = "\"" + ProcessName + "\" /A /V"
                    };
                    P.Start();
                    P.WaitForExit(60000);
                }*/
            }
        }

        private void NotifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void ShowForm()
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                //如果目前是縮小狀態，才要回覆成一般大小的視窗
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }
            // Activate the form.
            this.Activate();
            this.Focus();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            Close();
        }

        private void NotifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            ShowForm();
        }


    }
}
