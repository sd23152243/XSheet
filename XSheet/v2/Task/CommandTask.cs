using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XSheet.Data;
using XSheet.v2.Privilege;

namespace XSheet.v2.Task
{
    public class CommandTask:TaskObserver
    {
        public String taskID { get; set; }
        public XCommand cmd { get; set; }
        public DateTime submitTime { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public XSheetUser user { get; set; }
        public String statu { get; set; }
        public UserPrivilege up { get; set; }
        private CommandTask(){}

        public CommandTask(XCommand cmd, XSheetUser user)
        {
            this.cmd = cmd;
            this.user = user;
            this.submitTime = DateTime.Now;
        }
        
        public String doTask()
        {
            String ans = "";
            if (cmd.Async == true)
            {
                ThreadStart threadStart = new ThreadStart(doTaskAsync);
                Thread thread = new Thread(threadStart);
                thread.Start();
                return "OK";
            }
            else
            {
                ans = doTaskSync();
                return ans;
            }
        }

        private String doTaskSync()
        {
            String ans = cmd.execute(user);
            return ans;
        }

        private void doTaskAsync()
        {
            String ans = cmd.execute(user);
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
