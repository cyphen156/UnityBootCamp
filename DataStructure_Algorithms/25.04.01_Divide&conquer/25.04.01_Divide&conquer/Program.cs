using System.Drawing;
using System.Runtime.ExceptionServices;

namespace _25._04._01_DivideAndConquer
{
    internal class Program
    {
        //static void callBack(int size)
        //{
        //    if (size <= 0)
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        callBack(size - 1);
        //        printStar(size);
        //    }
        //}

        //static void printStar(int size)
        //{
        //    for (int i = 0; i < size; i++)
        //    {
        //        Console.Write('*');
        //    }
        //    Console.WriteLine();
        //}
        //private static int Fibo(int v)
        //{
        //    if (v == 0)
        //    {
        //        return 0;
        //    }
        //    if (v == 1 || v == 2)
        //    {
        //        Console.WriteLine("*");
        //        return 1;
        //    }
        //    else
        //    {
        //        return Fibo(v-1) + Fibo(v-2);
        //    }
        //}
        static void Main(string[] args)
        {
            //    int T;
            //    string str = Console.ReadLine();
            //    Console.WriteLine(str);
            //    int a = 7;
            //    int b = 5;

            //    byte num = 5 ^ 12;
            //    Console.WriteLine(num);

            //    string str1 = "Hello";
            //    string str2 = "Hello";

            //    string[] strArr = str.Split(' ');
            //Console.WriteLine(Fibo(50));
            //int input = int.Parse(Console.ReadLine());
            //callBack(input);
            //Console.WriteLine();

            // 입력 최적화는 필요 없음 (C#은 기본적으로 느림)
            int n = int.Parse(Console.ReadLine());
            Queue q = new Queue(10); // 초기 용량 적당히 설정

            for (int i = 0; i < n; ++i)
            {
                string input = Console.ReadLine();
                string[] parts = input.Split(' ');

                string command = parts[0];

                switch (command)
                {
                    case "push":
                        int value = int.Parse(parts[1]);
                        q.EnQueue(value);
                        break;

                    case "pop":
                        Console.WriteLine(q.DeQueue());
                        break;

                    case "size":
                        Console.WriteLine(q.Size());
                        break;

                    case "empty":
                        Console.WriteLine(q.Empty());
                        break;

                    case "front":
                        Console.WriteLine(q.Front());
                        break;

                    case "back":
                        Console.WriteLine(q.Back());
                        break;
                }
            }

        }

    }
}
