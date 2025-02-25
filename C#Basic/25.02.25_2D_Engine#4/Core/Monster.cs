using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25._02._25_2D_Engine_4.Core
{
    public class Monster : GameObject
    {
        public Monster(int inX, int inY, char inShape) : base(inX, inY, inShape)
        {
            layer = 2;
        }

        public void GameOver()
        {
            Engine.GetInstance().ShutDown();
        }
    }
}
