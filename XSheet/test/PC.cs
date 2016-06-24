using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XSheet.test
{
    class PC
    {
        private CPU cpu;
        private HardDisk HD;
        private int a;
        
        public void setCPU(CPU c)
        {   
            this.cpu = c;
        }
        public void setHardDisk(HardDisk h)
        {
            this.HD = h;
        }
        public void show()
        {
            string show = "CPU:" + cpu.getSpeed().ToString() 
                + "硬盘容量:" + HD.getAmount().ToString();
            System.Windows.Forms.MessageBox.Show(show);
        }

        /*  public int getA()
          {
              return this.a;
          }

          public void setA(int a)
          {
              this.a = a;
          }

          public void  showA()
          {
              int a = new int();
              MessageBox.Show(a.ToString());
          }*/
    }
}

