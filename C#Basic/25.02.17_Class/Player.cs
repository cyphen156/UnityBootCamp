using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250217
{
    public class Player : GameObject
    {
        public Player(int inX, int inY, char inShape)
        {
            X = inX;
            Y = inY;
            Shape = inShape;
        }

        internal static void Move()
        {
        }

        public override void Update()
        {
            // 내가 생각핸 인풋 매니저
            if (Engine.Instance.GetKeyDown("W"))
            {
                Y--;
            }
            else if (Engine.Instance.GetKeyDown("S"))
            {
                Y++;
            }
            else if (Engine.Instance.GetKeyDown("A"))
            {
                X--;
            }
            else if (Engine.Instance.GetKeyDown("D"))
            {
                X++;
            }
        }
    }
}
