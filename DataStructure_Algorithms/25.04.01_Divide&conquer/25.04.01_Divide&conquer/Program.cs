using System.Drawing;
using System.Runtime.ExceptionServices;

namespace _25._04._01_DivideAndConquer
{
    internal class Program
    {
        static void callBack(int size)
        {
            if (size <= 0)
            {
                return;
            }
            else
            {
                callBack(size - 1);
                printStar(size);
            }
        }

        static void printStar(int size)
        {
            for (int i = 0; i < size; i++)
            {
                Console.Write('*');
            }
            Console.WriteLine();
        }
        private static int Fibo(int v)
        {
            if (v == 0)
            {
                return 0;
            }
            if (v == 1 || v == 2)
            {
                Console.WriteLine("*");
                return 1;
            }
            else
            {
                return Fibo(v-1) + Fibo(v-2);
            }
        }
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
            Console.WriteLine(Fibo(50));
            //int input = int.Parse(Console.ReadLine());
            //callBack(input);
            //Console.WriteLine();
        }

    }
}
