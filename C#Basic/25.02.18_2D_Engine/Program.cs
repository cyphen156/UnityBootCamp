using _25._02._18_2D_Engine.Core;
using _25._02._18_2D_Engine.Data;

namespace _25._02._18_2D_Engine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List list = new List();

            for (int i = 0; i < 23; ++i)
            {
                list.Add(i);

            }

            for (int i = 0; i < 10; ++i)
            {
                list.Delete();
            }
            list.Count();
            list.Print();

            list.Insert(9, 5);
            list.RemoveAt(7);
            list.Clear();
            Engine engine = Engine.GetInstance();

            engine.Init();

            while (engine.isRunning)
            {
                engine.Run();
            }
        }
    }
}
