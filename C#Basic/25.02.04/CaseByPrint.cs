namespace _25._02._04
{
    internal class CaseByPrint
    {
        
        static void Main(string[] args)
        {
            Random rand = new Random();

            int[] array = new int[52];
            // 숫자배열 초기화
            for (int i = 0; i < array.Length; ++i)
            {
                array[i] = i + 1;
            }

            string str = "";
            int size = 1;
            // 카드뽑기
            for (int i = 0; i < size; ++i)
            {
                int choose = rand.Next(array.Length);
                //int choose = 25;
                if (choose < 14)
                {
                    str += "Heart ";
                }
                else if(choose < 27)
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
                else if (choose == 13)
                {
                    str += "K";
                }
                else
                {
                    str += choose % 13;
                }
                Console.Write(str);
            }
            Console.WriteLine();
        }
    }
}
