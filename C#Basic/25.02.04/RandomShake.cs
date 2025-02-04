namespace _25._02._04
{
    internal class RandomShake
    {
        // 메인 아이디어는 쉐이킹
        // 52개를 전체를 다 섞는다면 최악의 경우 초기화 52 + 자리교체 52 + 8회 뽑기
        // 눈속수 자리교체 52+8+8
        // 더 줄일 방법 여쭤보자
        static void Main(string[] args)
        {
            Random rand = new Random();

            int[] array = new int[52];
            // 숫자배열 초기화
            for (int i = 0; i < array.Length; ++i)
            {
                array[i] = i + 1;
            }
            // 전체 자리교체 코드
            //for (int i = 0; i < array.Length; ++i)
            //{
            //    int idx = rand.Next(array.Length);
            //    int temp = array[idx];
            //    array[idx] = array[i];
            //    array[i] = temp;
            //}

            // 눈속임 코드
            ///**어차피 출력은 8개만 하니까 8개만 바꿔놔도 다른건 확인 못한다
            for (int i = 0; i < 8; ++i)
            {
                int idx = rand.Next(array.Length);
                int temp = array[idx];
                array[idx] = array[i];
                array[i] = temp;
            }
            // 더 줄일 방법은 뭐가 있을까?
            // 출력 코드 
            for (int i = 0; i < 8; ++i)
            {
                Console.Write(array[i] + "\t");
            }
            Console.WriteLine();
        }
    }
}
