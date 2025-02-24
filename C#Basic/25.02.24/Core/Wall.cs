using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25._02._24.Core
{
    public class Wall : GameObject
    {
        public Wall(int inX, int inY, char inShape) : base(inX, inY, inShape)
        {
            layer = 1;
        }
    }
}
