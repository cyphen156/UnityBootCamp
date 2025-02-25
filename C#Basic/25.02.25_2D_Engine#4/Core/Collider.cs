using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25._02._25_2D_Engine_4.Core
{
    public class Collider : Component
    {
        public GameObject Owner
        {
            get; set;
        }

        public bool IsCollisionEnter(Collider other)
        {
            return Owner.newX == other.Owner.x && Owner.newY == other.Owner.y;
        }
    }
}
