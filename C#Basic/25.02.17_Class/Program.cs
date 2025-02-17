using System.Drawing;

namespace L20250217
{
    internal class Program
    {
        class Singleton
        {
            static Singleton instance;
            private Singleton() { }

            static public Singleton GetInst()
            {
                if (instance == null)
                {
                    instance = new Singleton();
                }

                return instance;
            }
        }

        static void Main(string[] args)
        {

            Engine engine = Engine.Instance;
            
            engine.Load();
            
            engine.Run();

            //engine.Stop();
        }
    }
}
