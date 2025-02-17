using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25._02._17_Class.Polymorphism
{
    public class Base
    {
        public Base() { }

        public int gold;

        public void Do()
        {
            gold++;
        }
    }

    public class Derived : Base
    {
        public void Do1()
        {
            gold--;
        }
    }
}
