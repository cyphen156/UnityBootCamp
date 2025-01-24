namespace _25._01._24
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //char wall = '*';
            //char floor = ' ';
            //bool condition = true;
            //// type 0: 돈까스, 1: 제육, 2: 짜장, 3: 짬뽕 
            //int type = 0;
            //type = 1;
            //type = 2;
            //type = 3;

            //int inputNum;
            //inputNum = int.Parse(Console.ReadLine());

            //if (inputNum % 4 == 0)
            //{
            //    Console.WriteLine($"{inputNum}은 4의 배수입니다.");
            //}
            //else
            //{
            //    Console.WriteLine($"{inputNum}은 4의 배수가 아닙니다.");
            //}

            //string str = "";

            //for (int i = 0; i < 5; i++)
            //{
            //    str += "*";
            //}
            //Console.WriteLine(str);

            int sum = 0, even = 0, odd = 0;

            for (int i = 0; i < 100; ++i)
            {
                sum += i+1;
                if((i & 0x00000001) != 0)
                {
                    even += i + 1;
                }
                else
                {
                    odd += i + 1;
                }
            }
            Console.WriteLine($"sum = {sum}\nevne = {even}\nodd = {odd}");
        }
    }
}
