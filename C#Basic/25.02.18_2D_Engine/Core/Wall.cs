using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25._02._18_2D_Engine.Core
{
    public class Wall : GameObject
    {
        public Wall(int inX, int inY, char inShape) : base(inX, inY, inShape)
        {
            layer = 1;
        }
    }
}
