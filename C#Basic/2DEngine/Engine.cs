using _2DEngine;
using SDL2;
using System;
using System.Numerics;
using System.Threading;

namespace _2DEngine
{
    public class Engine
    {
        private Engine()
        {

        }

        static protected Engine instance;

        //더블 버퍼링
        static public char[,] backBuffer = new char[20, 40];
        static public char[,] frontBuffer = new char[20, 40];

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

        public IntPtr myWindow;
        public IntPtr myRenderer;
        public SDL.SDL_Event myEvent;

        public bool Init()
        {
            if (SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING) < 0)
            {
                Console.WriteLine("Fail Init.");
                return false;
            }

            myWindow = SDL.SDL_CreateWindow(
                "Game",
                100, 100,
                640, 480,
                SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

            myRenderer = SDL.SDL_CreateRenderer(myWindow, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED |
                SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC |
                SDL.SDL_RendererFlags.SDL_RENDERER_TARGETTEXTURE);

            return true;
        }

        public bool Quit()
        {
            SDL.SDL_DestroyRenderer(myRenderer);
            SDL.SDL_DestroyWindow(myWindow);

            SDL.SDL_Quit();

            return true;
        }


        public void Load(string filename)
        {
            //string tempScene = "";
            //byte[] buffer = new byte[1024];
            //FileStream fs = new FileStream("level01.map", FileMode.Open);

            //fs.Seek(0, SeekOrigin.End);
            //long fileSize = fs.Position;

            //fs.Seek(0, SeekOrigin.Begin);
            //int readCount = fs.Read(buffer, 0, (int)fileSize);
            //tempScene = Encoding.UTF8.GetString(buffer);
            //tempScene = tempScene.Replace("\0", "");
            //string[] scene = tempScene.Split("\r\n");

            List<string> scene = new List<string>();

            StreamReader sr = new StreamReader(filename);
            while (!sr.EndOfStream)
            {
                scene.Add(sr.ReadLine());
            }
            sr.Close();


            world = new World();

            for (int y = 0; y < scene.Count; y++)
            {
                for (int x = 0; x < scene[y].Length; x++)
                {

                    GameObject floor = new GameObject();
                    floor.name = "Floor";
                    floor.transform.x = x;
                    floor.transform.y = y;

                    floor.AddComponent<PlayerController>(new PlayerController());
                    SpriteRenderer spriteRenderer = floor.AddComponent<SpriteRenderer>(new SpriteRenderer());
                    spriteRenderer.colorKey.r = 255;
                    spriteRenderer.colorKey.g = 255;
                    spriteRenderer.colorKey.b = 255;
                    spriteRenderer.colorKey.a = 255;
                    spriteRenderer.orderLayer = 0;
                    spriteRenderer.LoadBmp("floor.bmp");

                    spriteRenderer.Shape = ' ';

                    world.Instanciate(floor);
                    if (scene[y][x] == '*')
                    {
                        GameObject wall = new GameObject();
                        wall.name = "wall";
                        wall.transform.x = x;
                        wall.transform.y = y;

                        spriteRenderer = wall.AddComponent<SpriteRenderer>(new SpriteRenderer());
                        spriteRenderer.colorKey.r = 255;
                        spriteRenderer.colorKey.g = 255;
                        spriteRenderer.colorKey.b = 255;
                        spriteRenderer.colorKey.a = 255;
                        spriteRenderer.orderLayer = 1;
                        spriteRenderer.LoadBmp("wall.bmp");

                        spriteRenderer.Shape = '*';

                        world.Instanciate(wall);
                    }
                    else if (scene[y][x] == ' ')
                    {
                        
                    }
                    else if (scene[y][x] == 'P')
                    {
                        GameObject player = new GameObject();
                        player.name = "Player";
                        player.transform.x = x;
                        player.transform.y = y;

                        player.AddComponent<PlayerController>(new PlayerController());
                        player.AddComponent<Collider>(new Collider());
                        spriteRenderer = player.AddComponent<SpriteRenderer>(new SpriteRenderer());
                        spriteRenderer.colorKey.r = 255;
                        spriteRenderer.colorKey.g = 0;
                        spriteRenderer.colorKey.b = 255;
                        spriteRenderer.colorKey.a = 255;
                        spriteRenderer.orderLayer = 4;
                        spriteRenderer.LoadBmp("player.bmp", true);
                        spriteRenderer.processTime = 150.0f;
                        spriteRenderer.maxCellCountX = 5;

                        spriteRenderer.Shape = 'P';

                        world.Instanciate(player);
                    }
                    else if (scene[y][x] == 'M')
                    {
                        GameObject monster = new GameObject();
                        monster.name = "Monster";
                        monster.transform.x = x;
                        monster.transform.y = y;
                        monster.AddComponent<MonsterController>(new MonsterController());
                        monster.GetComponent<MonsterController>().processTime = 500f;
                        monster.AddComponent<Collider>(new Collider());
                        spriteRenderer = monster.AddComponent<SpriteRenderer>(new SpriteRenderer());
                        spriteRenderer.colorKey.r = 255;
                        spriteRenderer.colorKey.g = 255;
                        spriteRenderer.colorKey.b = 255;
                        spriteRenderer.colorKey.a = 255;
                        spriteRenderer.orderLayer = 2;
                        spriteRenderer.LoadBmp("monster.bmp");
                        spriteRenderer.processTime = 150.0f;

                        spriteRenderer.Shape = 'M';

                        world.Instanciate(monster);
                    }
                    else if (scene[y][x] == 'G')
                    {
                        GameObject goal = new GameObject();
                        goal.name = "Goal";
                        goal.transform.x = x;
                        goal.transform.y = y;

                        spriteRenderer = goal.AddComponent<SpriteRenderer>(new SpriteRenderer());
                        spriteRenderer.colorKey.r = 255;
                        spriteRenderer.colorKey.g = 255;
                        spriteRenderer.colorKey.b = 255;
                        spriteRenderer.colorKey.a = 255;
                        spriteRenderer.orderLayer = 3;
                        spriteRenderer.LoadBmp("goal.bmp");

                        spriteRenderer.Shape = 'G';

                        world.Instanciate(goal);
                    }
                }
            }

            //loading complete
            //sort
            world.Sort();
        }

        public void ProcessInput()
        {
            Input.Process();
        }


        protected void Update()
        {
            world.Update();
        }

        protected void Render()
        {
            SDL.SDL_SetRenderDrawColor(myRenderer, 0, 0, 0, 0);
            SDL.SDL_RenderClear(myRenderer);

            world.Render();

            for (int Y = 0; Y < 20; ++Y)
            {
                for (int X = 0; X < 40; ++X)
                {
                    if (Engine.frontBuffer[Y, X] != Engine.backBuffer[Y, X])
                    {
                        Engine.frontBuffer[Y, X] = Engine.backBuffer[Y, X];
                        Console.SetCursorPosition(X, Y);
                        Console.Write(frontBuffer[Y, X]);
                    }
                }
            }

            SDL.SDL_RenderPresent(myRenderer);

        }

        public void Run()
        {
            Console.CursorVisible = false;

            while (isRunning)
            {
                SDL.SDL_PollEvent(out myEvent);

                Time.Update();

                switch (myEvent.type)
                {
                    case SDL.SDL_EventType.SDL_QUIT:
                        isRunning = false;
                        break;
                }

                Update();
                Render();
            }
        }


        public World world;
    }
}