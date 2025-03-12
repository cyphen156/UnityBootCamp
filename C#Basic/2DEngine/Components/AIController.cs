using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DEngine
{
    /// <summary>
    /// Monster -> AIController
    /// </summary>
    class AIController : Component
    {
        private Random rand = new Random();
        public float processTime;
        Collider2D collider;
        CharacterController2D characterController2D;
        public AIController() { }
        ~AIController() { }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Console.WriteLine($"겸침 감지 : {other.gameObject.name}");
        }

        public override void Update()
        {
            if (gameObject.GetComponent<SpriteRenderer>().elapsedTime >= processTime)
            {
                int Direction = rand.Next(0, 4);

                collider = gameObject.GetComponent<Collider2D>();
                characterController2D = gameObject.GetComponent<CharacterController2D>();
                if (Direction == 0)
                {
                    // Legacy Collision
                    //if (!collider.PredictCollision(gameObject.transform.x, gameObject.transform.y - 1))
                    //{
                    //    gameObject.transform.y--;
                    //}
                    characterController2D.Move(0, -1);
                }
                if (Direction == 1)
                {
                    characterController2D.Move(0, 1);
                }
                if (Direction == 2)
                {
                    characterController2D.Move(-1, 0);
                }
                if (Direction == 3)
                {
                    characterController2D.Move(1, 0);
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
