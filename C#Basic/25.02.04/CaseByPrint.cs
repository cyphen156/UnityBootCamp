namespace _25._02._04
{
    internal class CaseByPrint
    {
        static int score = 0;

        static private void AddScore(int adder)
        {
            score += adder;
        }
        static private int GetScore()
        {
            return score;
        }
        static private string LotteryTypeCast (int choose)
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
            AddScore(choose);

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
        static void Main(string[] args)
        {
            Random rand = new Random();

            int[] deck = new int[52];
            // 숫자배열 초기화
            for (int i = 0; i < deck.Length; ++i)
            {
                deck[i] = i + 1;
            }

            int size = 2;

            // 카드뽑기
            for (int i = 0; i < size; ++i)
            {
                int choose = rand.Next(deck.Length);
                Console.WriteLine(LotteryTypeCast(deck[choose]));
            }

            Console.WriteLine();
            Console.WriteLine("Score : " + GetScore());
        }
    }
}
