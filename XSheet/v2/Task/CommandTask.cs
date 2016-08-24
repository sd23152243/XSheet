using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XSheet.Data;

namespace XSheet.v2.Task
{
    public class CommandTask:TaskObserver
    {
        public String taskID { get; set; }
        public XCommand cmd { get; set; }
        public DateTime submitTime { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public String owner { get; set; }
        public String statu { get; set; }

        private CommandTask(){}

        public CommandTask(XCommand cmd,String owner)
        {
            this.cmd = cmd;
            this.owner = owner;
            this.submitTime = DateTime.Now;
        }
        
        public void doTask()
        {
            if (cmd.sync == true)
            {
                doTaskSync();
            }
            else
            {
                doTaskAsync();
            }
        }

        private void doTaskSync()
        {
            cmd.execute();
        }

        private void doTaskAsync()
        {
            ThreadStart threadStart = new ThreadStart(doTaskSync);
            Thread thread = new Thread(threadStart);
            thread.Start();
        }

        public void UpdateStartime()
        {
            this.startTime = DateTime.Now;
        }

        public void UpdateEndtime()
        {
            this.endTime = DateTime.Now;
        }
    }
}
