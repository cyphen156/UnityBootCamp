using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25._02._25_2D_Engine_4.Core
{
    /// <summary>
    /// 싱글턴 엔진
    /// </summary>
    public class Engine
    {
        static public Engine instance;
        World world;
        InputManager inputManager;
        FileManager fileManager;
        public bool isRunning = true;

        static public char[,] backBuffer;
        static public char[,] frontBuffer;


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
            fileManager = FileManager.GetInstance();
            world = World.GetInstance();
            Load();
            Console.CursorVisible = false;
        }

        int count = 0;
        public void Run()
        {
            //Console.WriteLine(count++);
            if (Console.KeyAvailable)
            {
                Input();
            }
            world.Update();
            world.Render();
            //Console.WriteLine("*****************************\r\n+++++++++++++++++++++\r\n-------------------------*****************************\r\n+++++++++++++++++++++\r\n-------------------------*****************************\r\n+++++++++++++++++++++\r\n-------------------------*****************************\r\n+++++++++++++++++++++\r\n-------------------------*****************************\r\n+++++++++++++++++++++\r\n-------------------------*****************************\r\n+++++++++++++++++++++\r\n-------------------------*****************************\r\n+++++++++++++++++++++\r\n-------------------------*****************************\r\n+++++++++++++++++++++\r\n-------------------------*****************************\r\n+++++++++++++++++++++\r\n-------------------------*****************************\r\n+++++++++++++++++++++\r\n-------------------------*****************************\r\n+++++++++++++++++++++\r\n-------------------------");
        }
        public void Input()
        {
            inputManager.keyInfo = Console.ReadKey(false);
        }
        public void Load()
        {
            string path = "Data/level01.map";
            //file에서 로딩
            string[] scene = fileManager.ReadFileByByte(path);
            //string[] scene = fileManager.ReadFileByStreamReader(path);
            backBuffer = new char[scene.Length, scene[0].Length];
            frontBuffer = new char[scene.Length, scene[0].Length];

            for (int y = 0; y < scene.Length; y++)
            {
                for (int x = 0; x < scene[y].Length; x++)
                {
                    if (scene[y][x] == '*')
                    {
                        Wall wall = new Wall(x, y, scene[y][x]);
                        world.Instantiate(wall);
                        wall.AddComponent<Collider>();
                    }
                    else if (scene[y][x] == ' ')
                    {
                        Floor floor = new Floor(x, y, scene[y][x]);
                        world.Instantiate(floor);
                    }
                    else if (scene[y][x] == 'P')
                    {
                        Player player = new Player(x, y, scene[y][x]);
                        world.Instantiate(player);
                        player.AddComponent<Collider>();
                    }
                    else if (scene[y][x] == 'M')
                    {
                        Monster monster = new Monster(x, y, scene[y][x]);
                        world.Instantiate(monster);
                        monster.AddComponent<Collider>();
                    }
                    else if (scene[y][x] == 'G')
                    {
                        Goal goal = new Goal(x, y, scene[y][x]);
                        world.Instantiate(goal);
                        goal.AddComponent<Collider>();
                    }
                }
            }
            world.SelectionSort();
        }
        public void ShutDown()
        {
            isRunning = false;
        }
    }
}
