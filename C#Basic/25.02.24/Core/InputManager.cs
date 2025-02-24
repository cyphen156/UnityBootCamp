using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25._02._24.Core
{
    public class InputManager
    {
        private static InputManager instance;

        public ConsoleKeyInfo keyInfo;

        private InputManager() { }

        public static InputManager GetInstance()
        {
            if (instance == null)
            {
                instance = new InputManager();
            }
            return instance;
        }

        public ConsoleKeyInfo GetKeyDown()
        {
            return keyInfo;

            //keyInfo = Console.ReadKey();
            //// 월드는 게임오브젝트를 가지고 있고, 게임오브젝트 안에는 플레이어가 있다.
            //World world = World.GetInstance();
            //// 일단 널이 아니게 만들어야 하니까 냅따 집어넣고
            //Player player = null;

            //foreach (GameObject go in world.gameObjects)
            //{
            //    if (go is Player p)
            //    {
            //        player = p;
            //        break;
            //    }
            //}
            //// 없으면 함수종료
            //if (player == null)
            //{
            //    Console.WriteLine("플레이어가 없는데용?");
            //    return;
            //}

            //if (keyInfo.Key == ConsoleKey.W)
            //{
            //    player.SetPositionY(-1);
            //}
            //else if (keyInfo.Key == ConsoleKey.S)
            //{
            //    player.SetPositionY(1);
            //}
            //else if (keyInfo.Key == ConsoleKey.A)
            //{
            //    player.SetPositionX(-1);
            //}
            //else if (keyInfo.Key == ConsoleKey.D)
            //{
            //    player.SetPositionX(1);
            //}
        }
    }
}
