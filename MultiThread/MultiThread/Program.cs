using Microsoft.VisualBasic;
using MultiThread.Application;
using System.Threading.Tasks;

namespace MultiThread
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Multithreading application entry point
            // Initialize the application instance

            Thread App_01 = new Thread(App.Instance.Run) { Name = "App" }
            ;
            Thread Sub_01 = new Thread(SubThreadApplication.Instance.Run) { Name = "Sub_01" };

            App_01.Start();
            Sub_01.Start();

            Sub_01.Join();
            App_01.Join();

            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationTokenSource ctss = new CancellationTokenSource(TimeSpan.FromMicroseconds(5000));

            await Task.Run(() =>
            {
                while (true)
                {
                    if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.Escape)
                    {
                        Console.WriteLine("태스크 중단 요청");
                        cts.Cancel();
                        Console.WriteLine("토큰 상용해서 중단 확인");
                    }
                }
            });

            Task loadTask = FakeLoadSomethingAsync(cts.Token);
            Task timeoutTask = Task.Delay(5000, ctss.Token);

            await Task.WhenAny(loadTask, timeoutTask);  // 둘중 하나만 기다리는 녀석 먼저끝나는거 잇으면 종료
                                                        // 작업 성공한 녀석 반환함
            await Task.WhenAll(loadTask, timeoutTask);  // 둘 다 완료될 때까지 기다림
            
            return;
        }
        
        static async Task FakeLoadSomethingAsync(CancellationToken cancellationToken)
        {
            try
            {
                // Simulate some work
                for (int i = 0; i < 10; i++)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        Console.WriteLine("작업이 취소되었습니다.");
                        return;
                    }
                    Console.WriteLine($"작업 진행 중: {i + 1}");
                    await Task.Delay(1000, cancellationToken); // Simulate work
                }
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("작업이 취소되었습니다.");
            }
        }
    }
}
    
