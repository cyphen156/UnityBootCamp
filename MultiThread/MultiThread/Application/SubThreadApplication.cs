using System.Diagnostics;

namespace MultiThread.Application
{
    internal class SubThreadApplication
    {
        public static SubThreadApplication Instance { get; private set; }
        public static bool IsRunning { get; private set; } = false;

        static SubThreadApplication()
        {
            if (Instance == null)
            {
                Instance = new SubThreadApplication();
                IsRunning = true;
            }
            else
            {
                IsRunning = false;
                return;
            }
        }
        public void Run()
        {
            Stopwatch sw = new Stopwatch();
            for (int i = 0; i < 1000000; i++)
            {
                sw.Start();
                Console.WriteLine($"Sub-thread iteration {i + 1} in {sw.ElapsedTicks}");
                sw.Stop();
                //System.Threading.Thread.Sleep(500); // Simulate work
            }
            Console.WriteLine("Sub-thread application is running...");
        }
    }
}
