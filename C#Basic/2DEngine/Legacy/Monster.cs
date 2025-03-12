using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DEngine.Legacy
{
    /// <summary>
    /// Monster -> AIController
    /// </summary>
    public class Monster : GameObject
    {
        private Random rand = new Random();

        public Monster(int inX, int inY, char inShape)
        {
            //X = inX;
            //Y = inY;
            //Shape = inShape;
            //orderLayer = 5;
            //isTrigger = true;

            //color.r = 100;
            //color.g = 100;
            //color.b = 100;
            //LoadBmp("monster.bmp");

        }

        public override void Update()
        {
            //    if (elapsedTime >= 500.0f)
            //    {
            //        int Direction = rand.Next(0, 4);

            //        if (Direction == 0)
            //        {
            //            if (!PredictCollision(X, Y - 1))
            //            {
            //                Y--;
            //            }
            //        }
            //        if (Direction == 1)
            //        {
            //            if (!PredictCollision(X, Y + 1))
            //            {
            //                Y++;
            //            }
            //        }
            //        if (Direction == 2)
            //        {
            //            if (!PredictCollision(X - 1, Y))
            //            {
            //                X--;
            //            }
            //        }
            //        if (Direction == 3)
            //        {
            //            if (!PredictCollision(X + 1, Y))
            //            {
            //                X++;
            //            }
            //        }
            //        elapsedTime = 0;
            //    }
            //    else
            //    {
            //        elapsedTime += Time.deltaTime;
            //    }

            //    Console.SetCursorPosition(30, 10);
            //    Console.Write(Time.deltaTime);
            //}
        }
    }
}