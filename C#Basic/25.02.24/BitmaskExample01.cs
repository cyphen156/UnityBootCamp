using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25._02._24
{
    class BitmaskExample01
    {
        public void Solve()
        {
            // 40분
            int n = int.Parse(Console.ReadLine());

            int[] arr1 = new int[n];
            int[] arr2 = new int[n];

            // 입력처리
            string[] input1 = Console.ReadLine().Split();
            for (int i = 0; i < n; ++i)
            {
                arr1[i] = int.Parse(input1[i]);
            }

            string[] input2 = Console.ReadLine().Split();
            for (int i = 0; i < n; ++i)
            {
                arr2[i] = int.Parse(input2[i]);
            }

            int[] result = new int[n];

            for (int i = 0; i < n; ++i)
            {

                result[i] = arr1[i] | arr2[i];
                for (int j = n; j > 0; --j)
                {
                    if ((result[i] & (1 << j - 1)) != 0)
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }

    }
}
