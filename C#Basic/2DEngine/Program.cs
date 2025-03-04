using _2DEngine;
using System;
using System.Text;

namespace _2DEngine
{
    public class Program
    {

        static void Main(string[] args)
        {
            Engine.Instance.Init();

            Engine.Instance.Load("level01.map");
            Engine.Instance.Run();

            Engine.Instance.Quit();
        }
    }
}