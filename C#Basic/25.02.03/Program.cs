using System;

namespace _25._02._03
{
    internal class Program
    {
        private static T Multiply<T> (T lVal, T rVal)
        {
            T result = lVal;
            result *= rVal;
            return result;
        }
        static int Multiply(int lVal, int rVal)
        {
            return lVal * rVal;
        }

        static void Main(string[] args)
        {
            //int size = 5;
            //for (int i = 0; i < size; ++i)
            //{
            //    for (int j = i; j < size-1; ++j)
            //    {
            //        Console.Write(' ');
            //    }
            //    for (int j = 0; j <= i; ++j)
            //    {
            //        Console.Write('*');
            //    }
            //    Console.WriteLine();
            //}
            //int size = 10;
            //int[] data = new int[10];
            //for (int i = 0; i < size; i++)
            //{
            //    for (int j = 0; j < size; j++)
            //    {
            //        data[i] = i + 1;
            //    }
            //}

            int[,] data = new int[10, 10];

            int cnt = 1;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    data[i, j] = cnt;
                    cnt++;
                }
            }

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(data[i, j] + "\t");
                }
                Console.WriteLine();
            }

            Multiply(2.3f, 3.5f);
        }
    }
}
