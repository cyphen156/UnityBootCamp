using System.Data;

namespace _25._04._21_DataStructure_LiveShare
{
    class Program
    {
        static int N;
        static string[] Map = new string[25];
        static bool[,] isVisited = new bool[25, 25];

        static int MakeTown(int x, int y)
        {
            int result = 1;

            // 기저 조건들
            // #1 좌표가 유효 하지 않음  
            if (x < 0 || y < 0 || x >= N || y >= N)
            {
                return 0;
            }
            // #2 이미 방문한 경우
            if (isVisited[x, y])
            {
                return 0;
            }
            // #3 집이 아닌 경우
            if (Map[x][y] != '1')
            {
                return 0 ;
            }

            isVisited[x, y] = true;

            result += MakeTown(x - 1, y);
            result += MakeTown(x + 1, y);
            result += MakeTown(x, y - 1);
            result += MakeTown(x, y + 1);

            return result;
        }
       
        public static void Main()
        {
            N = int.Parse(Console.ReadLine());

            //List<int[]> arr = new List<int[]>();

            for (int i = 0; i < N; ++i)
            {
                //string str = Console.ReadLine();
                //int[] inputs = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
                //arr.Add(inputs);
                Map[i] = Console.ReadLine();
            }

            int townCount = 0;
            List<int> townHomes = new List<int>(); 
            for (int i = 0; i < N; ++i)
            {
                for (int j = 0; j < N; ++j)
                {
                    if (isVisited[i, j] == false && Map[i][j] == '1')
                    {
                        townHomes.Add(MakeTown(i, j));
                        ++townCount;
                    }
                }
            }
            Console.WriteLine(townCount);
            townHomes.Sort();
            for (int i = 0; i < townCount; ++i)
            {
                Console.WriteLine(townHomes[i].ToString());
                //Console.WriteLine("TownCount : " + i + 1.ToString() + "\nInclude Homes : " + townHomes[i].ToString());
            }
        }
    }
}
