using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25._02._24
{
    class BitmaskExample02
    {
        public void Solve()
        {
            // 20분
            // 2^63 - 1 = ==> 최댓값 오버플로우 되니까 ULL 또는 문자 배열 입력처리 필요

            // => 블록단위 입력 처리
            // if 00001111222222223333333344444444
            // ==> arr[0] = 00001111
            // ==> arr[1] = 22222222
            // ==> arr[2] = 33333333
            // ==> arr[3] = 44444444

            // 문자 배열 입력처리방식(Legacy)
            // if 00001111222222223333333344444444
            // int size = 32;
            // char[size] {0, 0, 0, 0, 1, 1, 1, 1
            //           , 2, 2, 2, 2, 2, 2, 2, 2
            //           , 3, 3, 3, 3, 3, 3, 3, 3
            //           , 4, 4, 4, 4, 4, 4, 4, 4}



            ulong n = ulong.Parse(Console.ReadLine());
            // 동일한 입력 처리 == int.TryParse(Console.ReadLine(), N);
            ulong[] arr = new ulong[n];
            ulong result = 0;
            for (int i = 0; i < arr.Length; ++i)
            {
                ulong x = ulong.Parse(Console.ReadLine());

                arr[i] = 2;
                while (true)
                {
                    if (x <= arr[i])
                    {
                        break;
                    }
                    arr[i] = arr[i] << 1;
                }
            }
            for (int i = 0; i < arr.Length; ++i)
            {
                result = result ^ arr[i];
            }
            Console.WriteLine(result);
        }
    }
}
