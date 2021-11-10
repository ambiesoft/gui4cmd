using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Ambiesoft.gui4cmd
{
    interface IThread2Main
    {
        int GetCurrentSessID();
        void OnProcessCreated(int sessID, Process process);
    }
    class ThreadData
    {
        private int _sessId;
        private IThread2Main _thread2main;

        public ThreadData(int sessId, IThread2Main thread2Main)
        {
            _sessId = sessId;
            _thread2main = thread2Main;
        }
        public void OnProcessCreated(Process process)
        {
            _thread2main.OnProcessCreated(_sessId, process);
        }
        public int SessID
        {
            get { return _sessId; }
        }
        public bool ShouldThreadAlive
        {
            get { return _sessId == _thread2main.GetCurrentSessID(); }
        }
    }
}
