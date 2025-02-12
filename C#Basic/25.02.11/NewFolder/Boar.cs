using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250211
{
    public class Boar : Monster
    {
        public Boar()
        {
        }

        ~Boar() { }

        public override void Move()
        {
            Console.WriteLine("멧돼지는 뛰댕긴다.");
        }
    }
}
