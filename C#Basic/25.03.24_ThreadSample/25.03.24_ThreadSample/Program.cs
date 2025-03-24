using System.Threading;

namespace _25._03._24_ThreadSample
{
    class Program
    {
        static Object locker = new Object();
        static Object locker2 = new Object();   
        volatile static int i = 0;
        static void A()
        {
            lock (locker)
            {
                lock (locker2)
                {
                    for (int j = 0; j < 1000; ++j)
                    //while (i < 10000)
                    {
                        Console.WriteLine("A" + ++i);
                    }
                }
            }
            // ==> InterLock
            //Interlocked.Increment(ref i);
        }

        static void B()
        {
            // 데드락을 피한 상태 --> 순환형 구조 탈출
            // 순환형 구조란? lock, lock2 :: lock2, lock1
            // 서로 반납을 못하는 상태
            lock (locker)
            {
                lock (locker2)
                {
                    for (int j = 0; j < 1000; ++j)
                    //while (i < 10000)
                    {
                        Console.WriteLine("B" + ++i);
                    }
                }
            }
        }

        // foreground, main thread 종료되면 나머지 쓰레드 다 종료
        static void Main(string[] args)
        {
            Thread thread01 = new Thread(new ThreadStart(A));
            Thread thread02 = new Thread(new ThreadStart(B));

            // 함수 따로 실행 시켜줘 (Thread) ==> OS에 부탁함
            thread01.IsBackground = true;
            thread01.Start();

            thread02.IsBackground = true;
            thread02.Start();


            // 스레드 마무리 되기 전까지 여기서 대기중
            thread01.Join();
            thread02.Join();
        }
    }
}
