using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSheet.v2.Task
{
    public abstract class TaskSubject
    {
        protected IList<TaskObserver> observers = new List<TaskObserver>();

        /// <summary>
        /// 增加观察者
        /// </summary>
        /// <param name="observer"></param>
        public void Attach(TaskObserver observer)
        {
            observers.Add(observer);
        }

        /// <summary>
        /// 移除观察者
        /// </summary>
        /// <param name="observer"></param>
        public void Detach(TaskObserver observer)
        {
            observers.Remove(observer);
        }

        /// <summary>
        /// 向观察者（们）发出通知
        /// </summary>
        public void StartNotify()
        {
            foreach (TaskObserver item in observers)
            {
                item.UpdateStartime();
            }
        }

        public void FinishNotify()
        {
            foreach (TaskObserver item in observers)
            {
                item.UpdateEndtime();
            }
        }
}
}
