using System.Numerics;

namespace _25._02._04
{
    class Player
    {
        private int score = 0;

        public void AddScore(int adder)
        {
            score += adder;
        }
        public int GetScore()
        {
            return score;
        }
    }
    internal class CaseByPrint
    {
        static private string LotteryTypeCast (int choose, Player player)
        {
            string str = "";

            //int choose = 52;
            str += "실제로 뽑힌 숫자 : " + choose + "\t";
            if (choose < 14)
            {
                str += "Heart ";
            }
            else if (choose < 27)
            {
                str += "Diamond ";
            }
            else if (choose < 40)
            {
                str += "Clover ";
            }
            else
            {
                str += "Spade ";
            }


            // 뽑힌 카드 숫자 더하기
            choose %= 13;
            player.AddScore(choose);

            if (choose == 1)
            {
                str += "A";
            }
            else if (choose == 11)
            {
                str += "J";

            }
            else if (choose == 12)
            {
                str += "Q";
            }
            else if (choose == 0)
            {
                str += "K";
            }
            else
            {
                str += choose;
            }
            return str;
        }

        static void printWin(string str)
        {
            System.Console.WriteLine("Winner :: " + str);
        }
        static void Main(string[] args)
        {
            Player player = new Player();
            Player dealer = new Player();

            Random rand = new Random();

            int[] deck = new int[52];
            // 숫자배열 초기화
            for (int i = 0; i < deck.Length; ++i)
            {
                deck[i] = i + 1;
            }

            int size = 3;

            // 플레이어 카드뽑기
            for (int i = 0; i < size; ++i)
            {
                int choose = rand.Next(deck.Length);
                Console.WriteLine(LotteryTypeCast(deck[choose], player));
            }
            Console.WriteLine("Player Score : " + player.GetScore());
            Console.WriteLine();

            // 딜러 카드뽑기
            for (int i = 0; i < size; ++i)
            {
                int choose = rand.Next(deck.Length);
                Console.WriteLine(LotteryTypeCast(deck[choose], dealer));
            }
            Console.WriteLine("Dealer Score : " + dealer.GetScore());
            Console.WriteLine();
            

            // 점수 비교
            if (player.GetScore() >= dealer.GetScore())
            {
                printWin("Player");
            }
            else
            {
                printWin("Dealer");
            }
        }
    }
}
