using System;

namespace _25._02._04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();

            int[] array = new int[52];
            //int[] printArray = new int[8];
            // 숫자배열 초기화
            for (int i = 0; i < array.Length; ++i)
            {
                array[i] = i+1;
            }
            // 확인배열 초기화

            //for (int i = 0; i < 8; ++i)
            //{
            //    printArray[i] = 0;
            //}
            for (int i = 0; i < 8; ++i)
            {
                int temp = rand.Next(array.Length);
                //for (int j = 0; j <= i; ++j)
                //{
                //    // 출현했으면 다시뽑아
                //    if (temp == printArray[j])
                //    {
                //        temp = rand.Next(array.Length);
                //    }
                //    // 처음 등장하는거면 탈출해
                //    else
                //    {
                //        printArray[j] = temp;
                //        Console.WriteLine(printArray[]);
                //        break;
                //    }
                //}
                
                // 출력 여부 확인하기
                int cnt = 0;
                while(true)
                {
                    // 등장 안했으니까 루프에서 나가
                    if (array[temp]!=-1)
                    {
                        break;
                    }
                    cnt++;
                    // 조건에 안걸리면 등장안한거니까 다시뽑아
                    temp = rand.Next(array.Length);
                }

                Console.Write(array[temp] + "\t" + cnt);
                // 등장 했으니까 값을 바꿔라
                array[temp] = -1;
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
