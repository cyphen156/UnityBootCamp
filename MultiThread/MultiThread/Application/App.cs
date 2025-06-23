using System.Diagnostics;

namespace MultiThread.Application
{
    internal class App
    {
        public static App Instance { get; private set; }
        public static bool IsRunning { get; private set; } = false;
        static App()
        {
            if (Instance == null)
            {
                Instance = new App();
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
                Console.WriteLine($"Applicion thread iteration {i + 1} in {sw.ElapsedTicks}");
                sw.Stop();
                //System.Threading.Thread.Sleep(500); // Simulate work
            }   
            Console.WriteLine("Application is running...");
        }
    }

}
