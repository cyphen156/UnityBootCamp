using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25._02._25_2D_Engine_4.Core
{
    public class Player : GameObject
    {
        
        public Player (int inX, int inY, char inShape) : base (inX, inY, inShape)
        {
            layer = 4;
        }

        // 현재 위치를 바꿔서 렌더링 하고 있지만 무조건 배열에 들어간 순서대로 렌더링중
        // -> 플레이어의 렌더링 순서를 바꿔야함
        public override void Update()
        {
            Console.WriteLine("업데이트 실행중");
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
            newX = x + moveX;
            newY = y + moveY;
            GameObject other = CanMove(newX, newY);
            if (other == null)
            {
                Console.WriteLine("바닥임");
            }
            else
            {
                Move(moveX, moveY);
                other.Move(-moveX, -moveY);
            }
            InputManager.GetInstance().ClearKeyInfo();
            Console.WriteLine( "업데이트 실행완룡ㅇㅇㅇㅇㅇㅇㅇ");
        }
    }
}
