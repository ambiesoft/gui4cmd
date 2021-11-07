using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Ambiesoft.gui4cmd
{
    public partial class FormMain : Form
    {
        Thread _thCmd;
        delegate void AddToLog(string txt);
        public FormMain()
        {
            InitializeComponent();

            tabRoot.Dock = DockStyle.Fill;
            txtOutput.Dock = DockStyle.Fill;
            txtError.Dock = DockStyle.Fill;
            tabRoot.TabPages[0].Controls.Add(txtOutput);
            tabRoot.TabPages[1].Controls.Add(txtError);
        }

        bool IsFormAlive
        {
            get
            {
                return this.Created && this.IsHandleCreated && !this.IsDisposed;
            }
        }
        void StartOfThread(object obj)
        {
            int retval;
            Process pro;
            try
            {
                AmbLib.OpenCommandGetResultCallback(
                    Program.App,
                    Program.Args,
                    Encoding.UTF8,
                    out retval,
                    new DataReceivedEventHandler(delegate (object sender, DataReceivedEventArgs e)
                    {
                        if (!IsFormAlive)
                            return;
                        if (e.Data == null)
                            return;
                        this.BeginInvoke(new AddToLog(delegate (string txt)
                        {
                            if (!IsFormAlive)
                                return;
                            txtOutput.AppendText(txt);
                            txtOutput.AppendText("\r\n");
                        }), e.Data);
                    }),
                    new DataReceivedEventHandler(delegate (object sender, DataReceivedEventArgs e)
                    {
                        if (!IsFormAlive)
                            return;
                        if (e.Data == null)
                            return;
                        this.BeginInvoke(new AddToLog(delegate (string txt)
                        {
                            if (!IsFormAlive)
                                return;
                            txtError.AppendText(txt);
                            txtError.AppendText("\r\n");
                        }), e.Data);
                    }),

                    // null,
                    new EventHandler(delegate (object sender, EventArgs e) { }),
                    out pro);
            }
            catch(Exception)
            {

            }
        }
            
        private void FormMain_Load(object sender, EventArgs e)
        {
            _thCmd = new Thread(new ParameterizedThreadStart(StartOfThread));
            _thCmd.Start(null);
        }
    }
}
