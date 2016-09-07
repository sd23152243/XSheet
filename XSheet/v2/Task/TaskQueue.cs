using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace XSheet.v2.Task
{
    public static class TaskQueue
    {
        private static Queue<CommandTask> q = new Queue<CommandTask>();
        private static ThreadStart threadStart = new ThreadStart(begin);
        private static Thread thread = new Thread(threadStart);
        private static int stopFlag = 0;
        public static void addTask(CommandTask task)
        {
            q.Enqueue(task);
        }

        public static CommandTask get1stTask()
        {
            if (q.Count>0)
            {
                return q.Dequeue();
            }
            return null;
        }

        public static void start()
        {
            thread.Start();
        }

        public static void stop()
        {
            stopFlag = 1;
        }


        private static void begin()
        {
            while (stopFlag ==0)
            {
                CommandTask task = get1stTask();
                if (task != null)
                {
                    task.doTask();
                }
                Thread.Sleep(1000);
            }
            stopFlag = 0;
        }
    }
}
