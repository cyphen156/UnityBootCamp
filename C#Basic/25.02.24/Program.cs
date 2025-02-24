using _25._02._24.Core;

namespace _25._02._24
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //BitmaskExample01 bmEx = new BitmaskExample01;

            //bmEx.Solve();

            //BitmaskExample02 bmEx2 = new BitmaskExample02;

            //bmEx2.Solve();

            Engine engine = Engine.GetInstance();

            engine.Init();

            while (engine.isRunning)
            {
                engine.Run();
            }
        }
    }
}
// 조건 체크 안했어요