using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SDL2.SDL;

namespace _2DEngine
{
    class PlayerController : Component
    {
        // Player -> PlayerController
        Collider2D collider;
        SpriteRenderer sprite;
        CharacterController2D characterCollider2D;

        public void OnTriggerEnter2D(Collider2D other)
        {
            Console.WriteLine($"겹침 감지 : {other.gameObject.name}");

            if (other.gameObject.name.CompareTo("Goal") == 0)
            {
                GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
                gameManager.isFinish = true;
            }

            if (other.gameObject.name.CompareTo("Monster") == 0)
            {
                GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

                GameObject.Find("GameManager").GetComponent<GameManager>().isFinish = true;
            }
        }

        public override void Update()
        {
            collider = gameObject.GetComponent<Collider2D>();
            sprite = gameObject.GetComponent<SpriteRenderer>();
            characterCollider2D = gameObject.GetComponent<CharacterController2D>();
            if (Input.GetKeyDown(SDL_Keycode.SDLK_w) || Input.GetKeyDown(SDL_Keycode.SDLK_UP))
            {

                sprite.spriteIndexY = 2;
                characterCollider2D.Move(0, -1);
                // Legacy Move()
                //if (!collider.PredictCollision(gameObject.transform.x, gameObject.transform.y - 1))
                //{
                //}
            }
            if (Input.GetKeyDown(SDL_Keycode.SDLK_s) || Input.GetKeyDown(SDL_Keycode.SDLK_DOWN))
            {
                sprite.spriteIndexY = 3;
                characterCollider2D.Move(0, +1);

            }
            if (Input.GetKeyDown(SDL_Keycode.SDLK_a) || Input.GetKeyDown(SDL_Keycode.SDLK_LEFT))
            {
                sprite.spriteIndexY = 0;
                characterCollider2D.Move(-1, 0);
            }
            if (Input.GetKeyDown(SDL_Keycode.SDLK_d) || Input.GetKeyDown(SDL_Keycode.SDLK_RIGHT))
            {
                sprite.spriteIndexY = 1;
                characterCollider2D.Move(1, 0);
            }
        }
    }
}
