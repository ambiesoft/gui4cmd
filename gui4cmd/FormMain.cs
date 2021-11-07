using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Ambiesoft.gui4cmd
{
    public partial class FormMain : Form
    {
        Thread _thCmd;
        delegate void AddToLog(string txt);

        Queue<string> _outputs = new Queue<string>();
        Queue<string> _errors = new Queue<string>();
        public FormMain()
        {
            InitializeComponent();

            tabRoot.Dock = DockStyle.Fill;
            txtOutput.Dock = DockStyle.Fill;
            txtError.Dock = DockStyle.Fill;
            tabRoot.TabPages[0].Controls.Add(txtOutput);
            tabRoot.TabPages[1].Controls.Add(txtError);

            Application.Idle += Application_Idle;

            timerToControl.Interval = 10;
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            // ApplyOutputs();
        }
        private void timerToControl_Tick(object sender, EventArgs e)
        {
            ApplyOutputsByOne();
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        void AddToOutputs(string txt)
        {
            _outputs.Enqueue(txt);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        void AddToErrors(string txt)
        {
            _errors.Enqueue(txt);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        void ApplyOutputsByOne()
        {
            if (_outputs.Count != 0)
            {
                txtOutput.AppendText(_outputs.Dequeue());
                txtOutput.AppendText("\r\n");
            }

            if (_errors.Count != 0)
            {
                txtError.AppendText(_errors.Dequeue());
                txtError.AppendText("\r\n");
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        void ApplyOutputsAll()
        {
            {
                StringBuilder sbOutputs = new StringBuilder();
                while (_outputs.Count != 0)
                    sbOutputs.AppendLine(_outputs.Dequeue());

                txtOutput.AppendText(sbOutputs.ToString());
            }
            {
                StringBuilder sbErrors = new StringBuilder();
                while (_errors.Count != 0)
                    sbErrors.AppendLine(_errors.Dequeue());

                txtError.AppendText(sbErrors.ToString());
            }
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
                        AddToOutputs(e.Data);
                    }),
                    new DataReceivedEventHandler(delegate (object sender, DataReceivedEventArgs e)
                    {
                        if (!IsFormAlive)
                            return;
                        if (e.Data == null)
                            return;
                        AddToErrors(e.Data);
                    }),

                    new EventHandler(delegate (object sender, EventArgs e) { }),
                    out pro);
            }
            catch(Exception)
            {

            }
            this.BeginInvoke(new EventHandler(delegate (object sender, EventArgs e)
            {
                // Thead Finished
                ApplyOutputsAll();
                timerToControl.Enabled = false;
            }), this, null);
        }
            
        private void FormMain_Load(object sender, EventArgs e)
        {
            _thCmd = new Thread(new ParameterizedThreadStart(StartOfThread));
            _thCmd.Start(null);
        }


    }
}
