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
    public partial class FormMain : Form, IThread2Main
    {
        Thread _thCmd;
        Process _process;
        delegate void AddToLog(string txt);
        volatile int _currentSessID;

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
        void AddToOutputs(int sessID, string txt)
        {
            if (sessID != _currentSessID)
                return;
            _outputs.Enqueue(txt);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        void AddToErrors(int sessID, string txt)
        {
            if (sessID != _currentSessID)
                return;
            _errors.Enqueue(txt);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        void ApplyOutputsByOne()
        {
            if (!IsFormAlive)
                return;
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
        void ApplyOutputsAll(int sessID)
        {
            if (sessID != _currentSessID)
                return;

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
            ThreadData threadData = (ThreadData)obj;
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
                        AddToOutputs(threadData.SessID, e.Data);
                    }),
                    new DataReceivedEventHandler(delegate (object sender, DataReceivedEventArgs e)
                    {
                        if (!IsFormAlive)
                            return;
                        if (e.Data == null)
                            return;
                        AddToErrors(threadData.SessID, e.Data);
                    }),

                    new EventHandler(delegate (object sender, EventArgs e) {
                        // sender is process
                        threadData.OnProcessCreated((Process)sender);
                    }),
                    out pro);
            }
            catch(Exception)
            {

            }

            if (IsFormAlive && threadData.ShouldThreadAlive)
            {
                this.BeginInvoke(new EventHandler(delegate (object sender, EventArgs e)
                {
                    // Thead Finished
                    ApplyOutputsAll(threadData.SessID);
                    timerToControl.Enabled = false;
                }), this, null);
            }
        }
            
        private void FormMain_Load(object sender, EventArgs e)
        {
            _thCmd = new Thread(new ParameterizedThreadStart(StartOfThread));
            _thCmd.Start(new ThreadData(++_currentSessID, this));
        }

        int IThread2Main.GetCurrentSessID()
        {
            return _currentSessID;
        }

        void OnProcessCreatedStuff(int sessID, Process process)
        {
            if (sessID != _currentSessID)
                return;
            _process = process;
        }
        void IThread2Main.OnProcessCreated(int sessID, Process process)
        {
            if (this.InvokeRequired)
            {
                EndInvoke(this.BeginInvoke(new Action(() => OnProcessCreatedStuff(sessID, process))));
                return;
            }
            OnProcessCreatedStuff(sessID, process);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(_process!=null && !_process.HasExited)
            {
                ++_currentSessID;
                _process.Kill();
                _thCmd.Join();
            }
        }
    }
}
