using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25._02._18_2D_Engine.Core
{
    /// <summary>
    /// 싱글턴 엔진
    /// </summary>
    public class Engine
    {
        static private Engine instance;
        World world;
        InputManager inputManager;
        public bool isRunning = true;
        private Engine() { }

        public static Engine GetInstance()
        {
            if (instance == null)
            {
                instance = new Engine();
            }
            return instance;
        }

        public void Init()
        {
            inputManager = InputManager.GetInstance();

            world = World.GetInstance();
            Load();
        }

        public void Run()
        {
            Input();
            world.Update();
            Console.Clear();
            world.Render();
        }
        public void Input()
        {
            inputManager.keyInfo = Console.ReadKey();
        }
        public void Load()
        {
            //file에서 로딩
            string[] scene = {
                "**********",
                "* P      *",
                "*        *",
                "*        *",
                "*        *",
                "*   M    *",
                "*        *",
                "*        *",
                "*       G*",
                "**********"
            };

            for (int y = 0; y < scene.Length; y++)
            {
                for (int x = 0; x < scene[y].Length; x++)
                {
                    if (scene[y][x] == '*')
                    {
                        Wall wall = new Wall(x, y, scene[y][x]);
                        world.Instanciate(wall);
                        wall.AddComponent<Collider>();
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
                        player.AddComponent<Collider>();
                    }
                    else if (scene[y][x] == 'M')
                    {
                        Monster monster = new Monster(x, y, scene[y][x]);
                        world.Instanciate(monster);
                        monster.AddComponent<Collider>();
                    }
                    else if (scene[y][x] == 'G')
                    {
                        Goal goal = new Goal(x, y, scene[y][x]);
                        world.Instanciate(goal);
                        goal.AddComponent<Collider>();
                    }
                }
            }
        }
        public void ShutDown()
        {
            isRunning = false;
        }
    }
}
