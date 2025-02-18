using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25._02._18_2D_Engine.Core
{
    public class Player : GameObject
    {
        World world = World.GetInstance();
        
        public Player (int inX, int inY, char inShape) : base (inX, inY, inShape)
        {
            layer = 4;
        }

        // 현재 위치를 바꿔서 렌더링 하고 있지만 무조건 배열에 들어간 순서대로 렌더링중
        // -> 플레이어의 렌더링 순서를 바꿔야함
        public override void Update()
        {
            int moveX = 0;
            int moveY = 0;

            ConsoleKeyInfo inputKey = InputManager.GetInstance().GetKeyDown();
            if (inputKey.Key == ConsoleKey.W)
            {
                moveY--;
            }
            else if (inputKey.Key == ConsoleKey.S)
            {
                moveY++;
            }
            else if (inputKey.Key == ConsoleKey.A)
            {
                moveX--;
            }
            else if (inputKey.Key == ConsoleKey.D)
            {
                moveX++;
            }
            Move(moveX, moveY);
            GameObject other = world.IsCollision(this);

            if (other is Monster monster)
            {
                monster.GameOver();
            }
            else if (other is Wall wall)
            {
                // 리터닝
                Move(-moveX, -moveY);
            }
            else if (other is Goal goal)
            {
                goal.NextGame();
            }
        }
        public void Move(int inX, int inY)
        {
            x += inX;
            y += inY;
        }
    }
}
