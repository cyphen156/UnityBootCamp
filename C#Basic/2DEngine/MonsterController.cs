using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DEngine
{
    /// <summary>
    /// Monster -> MonsterController
    /// </summary>
    class MonsterController : Component
    {
        private Random rand = new Random();
        public float processTime;
        public override void Update()
        {
            if (gameObject.GetComponent<SpriteRenderer>().elapsedTime >= processTime)
            {
                int Direction = rand.Next(0, 4);

                Collider collider = gameObject.GetComponent<Collider>();
                if (Direction == 0)
                {
                    if (!collider.PredictCollision(gameObject.transform.x, gameObject.transform.y - 1))
                    {
                        gameObject.transform.y--;
                    }
                }
                if (Direction == 1)
                {
                    if (!collider.PredictCollision(gameObject.transform.x, gameObject.transform.y + 1))
                    {
                        gameObject.transform.y++;
                    }
                }
                if (Direction == 2)
                {
                    if (!collider.PredictCollision(gameObject.transform.x - 1, gameObject.transform.y))
                    {
                        gameObject.transform.x--;
                    }
                }
                if (Direction == 3)
                {
                    if (!collider.PredictCollision(gameObject.transform.x + 1, gameObject.transform.y))
                    {
                        gameObject.transform.x++;
                    }
                }
                gameObject.GetComponent<SpriteRenderer>().elapsedTime = 0;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().elapsedTime += Time.deltaTime;
            }

            Console.SetCursorPosition(30, 10);
            Console.Write(Time.deltaTime);
        }
    }
}
