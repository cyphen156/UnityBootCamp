using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25._02._17_Class
{
    public class Input
    {
        static protected Input instance;
        static public Input Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Input();
                }
                return instance;
            }
        }
    }

}
