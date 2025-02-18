using _25._02._18_2D_Engine.Core;

namespace _25._02._18_2D_Engine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Engine engine = Engine.GetInstance();

            engine.Init();

            while (engine.isRunning)
            {
                engine.Run();
            }
        }
    }
}
