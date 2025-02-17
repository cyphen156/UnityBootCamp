using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250217
{
    public class Engine
    {
        static protected Engine instance;
        static public Engine Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Engine();
                }
                return instance;
            }
        }
        protected bool isRunning = true;

        protected ConsoleKeyInfo keyInfo;

        public void Load()
        {
            //file에서 로딩
            string[] scene = {
                "**********",
                "*P       *",
                "*        *",
                "*        *",
                "*        *",
                "*   M    *",
                "*        *",
                "*        *",
                "*       G*",
                "**********"
            };

            world = new World();

            for (int y = 0; y < scene.Length; y++)
            {
                for (int x = 0; x < scene[y].Length; x++)
                {
                    if ( scene[y][x] == '*' )
                    {
                        Wall wall = new Wall(x, y, scene[y][x]);
                        world.Instanciate(wall);
                    }
                    else if (scene[y][x] == ' ')
                    {
                        Floor floor = new Floor(x, y, scene[y][x]);
                        world.Instanciate(floor);
                    }
                    else if (scene[y][x] == 'P')
                    {
                        Player player = new Player(x, y, scene[y][x]);
                        world.Instanciate(player);
                    }
                    else if (scene[y][x] == 'M')
                    {
                        Monster monster = new Monster(x, y, scene[y][x]);
                        world.Instanciate(monster);
                    }
                    else if (scene[y][x] == 'G')
                    {
                        Goal goal = new Goal(x, y, scene[y][x]);
                        world.Instanciate(goal);
                    }
                }
            }
        }

        public void Input()
        {
            keyInfo = Console.ReadKey();
        }

        // 내가 생각한 GetKeyDown
        // 발생한 문제 keyInfo.Key는 구조체로 ConsoleKey값을 가지고 있기 때문에 문자열 비교연산 불가
        // => 강제 형변환으로 비교 가능
        public bool GetKeyDown(string input)
        {
            if (input == keyInfo.Key.ToString())
            {
                return true;
            }
            return false;
        }
        protected void Update()
        {
            world.Update();
        }

        protected void Render()
        {
            Console.Clear();
            world.Render();
        }


        public void Run()
        {
            while (isRunning)
            {
                Input();
                Update();
                Render();
            }
        }

        public World world;
    }
}
