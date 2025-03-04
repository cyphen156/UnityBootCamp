using _2DEngine;
using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DEngine
{
    public class Input
    {
        public Input()
        {

        }

        static public void Process()
        {
            //if (Console.KeyAvailable)
            //{
            //    keyInfo = Console.ReadKey(true);
            //}
        }

        static protected ConsoleKeyInfo keyInfo;

        static public bool GetKeyDown(ConsoleKey key)
        {
            return (keyInfo.Key == key);
        }

        static public bool GetKeyDown(SDL.SDL_Keycode key)
        {
            return (Engine.Instance.myEvent.key.keysym.sym == key);
        }

        public static void ClearInput()
        {
            keyInfo = new ConsoleKeyInfo();
        }
    }
}