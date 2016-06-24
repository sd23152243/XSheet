using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSheet.test
{
  public  class Person  //所有的类都会有一个隐藏的构造方法，与类同名且不需要返回类型。
    {
        private Person() //私有方法只能在类的内部调用
        {
            

        }
        public Person(String name,string sex,int age,float height,char core)
        {
            this.name = name;
            this.sex = sex;
            this.age = age;
            this.height = height;
            this.score = core;
        }
        public string name;
        public string sex;
        public int age;
        public float height;
        public char score;
        private void Speak()
        {
            String speakStr = "My name is " + this.name + "My sex is " + this.sex + "My age is " 
                    + this.age + "My height is" + this.height + "My Score is " + this.score;
            System.Windows.Forms.MessageBox.Show(speakStr);
        }

        public void Introduce()
        {
            Speak();
        }

        public void Introduce(String name)
        {
            this.name = name;
            System.Windows.Forms.MessageBox.Show("你改名字了！");;
        }

    }
    
}
