using _25._02._25_2D_Engine_4.Core;
using _25._02._25_2D_Engine_4.DataStructure;

namespace _25._02._25_2D_Engine_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //DynamicArray<string> dArray = new DynamicArray<string>();

            //for (int i = 0; i < 9; ++i)
            //{
            //    dArray.Add(i.ToString());
            //}

            //dArray.RemoveAt(7);

            //dArray.Count();

            //for (int i = 10; i < 25; ++i)
            //{
            //    dArray.Add(i.ToString());
            //}

            //DynamicArray<string> dArray2 = new DynamicArray<string>();
            //dArray.PrintSize();
            //DynamicArray<string> dArray3 = new DynamicArray<string>();
            //dArray2.PrintSize(); dArray3.PrintSize();
            Engine engine = Engine.GetInstance();

            engine.Init();

            while (engine.isRunning)
            {
                engine.Run();
            }

            //Console.WriteLine("콘솔라인출력하기");
            //Console.SetCursorPosition(0, 0);
            //Console.Write("CW");

        }
    }
}
