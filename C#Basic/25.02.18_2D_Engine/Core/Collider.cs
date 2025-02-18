using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25._02._18_2D_Engine.Core
{
    public class Collider
    {
        public GameObject Owner
        {
            get; set;
        }

        public bool IsCollisionEnter(Collider other)
        {
            return Owner.x == other.Owner.x && Owner.y == other.Owner.y;
        }
    }
}
