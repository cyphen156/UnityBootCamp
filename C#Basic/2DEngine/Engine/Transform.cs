using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DEngine
{
    public class Transform : Component
    {
        public int x;
        public int y;
        public override void Update()
        {
        }

        public void TransLate(int addX, int addY)
        {
            x += addX;
            y += addY;
        }

    }
}
