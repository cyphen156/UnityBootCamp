using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250217
{
    public class Monster : GameObject
    {
        Random rand = new Random();

        public Monster(int inX, int inY, char inShape)
        {
            X = inX;
            Y = inY;
            Shape = inShape;
        }

        public override void Update()
        {
            int value = rand.Next(0, 4);

            if (value == 0)
            {
                Y--;
            }
            else if (value == 1)
            {
                Y++;
            }
            else if (value == 2)
            {
                X--;
            }
            else if (value == 3)
            {
                X++;
            }
        }
    }
}
