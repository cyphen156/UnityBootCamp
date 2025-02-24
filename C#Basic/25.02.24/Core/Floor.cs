using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25._02._24.Core
{
    public class Floor : GameObject
    {
        public Floor(int inX, int inY, char inShape) : base(inX, inY, inShape)
        {
            isTrigger = false;
        }
    }
}
