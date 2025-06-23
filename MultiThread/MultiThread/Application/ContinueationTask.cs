/**
 * Task 완료 후 후속 작업을 연결할 때 사용하는 클래스
 * 
 * 요새는 잘 안쓴다고 한다.
 * 
 * ==> Async/Await 패턴을 사용하여 비동기 작업을 처리하는 것이 일반적임
 */

namespace MultiThread.Application
{
    public class ContinueationTask
    {
        public static void RunContinueationTask()
        {
            Task.Run(() =>
            {
                for (int i = 0; i < 1000000; i++)
                {
                    Console.WriteLine($"Continuation Task iteration {i + 1}");
                    // Simulate work
                    System.Threading.Thread.Sleep(1);
                }
            }).ContinueWith((task) =>
            {
                // 태스크가 중단 없이 완료된 경우
                if (task.IsCompletedSuccessfully)
                {
                    Console.WriteLine("Continuation Task completed successfully.");
                }

                // 태스크가 실패한 경우
                else if (task.IsFaulted)
                {
                    Console.WriteLine("Continuation Task encountered an error: " + task.Exception?.Message);
                }
            });
        }
    }
}
