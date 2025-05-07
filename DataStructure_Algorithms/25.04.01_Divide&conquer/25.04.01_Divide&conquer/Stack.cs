
namespace _25._04._01_DivideAndConquer
{
    internal class Stack
    {
        private int size;
        private int[] datas;

        public Stack(int initSize)
        {
            size = -1;
            datas = new int[initSize];
        }

        ~Stack() { }

        public void Push(int inData)
        {
            if (size == datas.Length)
            {
                int newLength = datas.Length * 2;
                int[] newDatas = new int[newLength];
                for (int i = 0; i < datas.Length; i++) 
                {
                    newDatas[i] = datas[i];
                }
                datas = newDatas;
            }
            size++;
            datas[size] = inData;
        }

        public int Pop()
        {
            if (IsEmpty())
            {
                return -1;
            }
            return datas[size--];
        }

        private bool IsEmpty()
        {
            if (size == -1)
            {
                return true;
            }
            return false;
        }
        public int Empty()
        {
            if (size == -1)
            {
                return 1;
            }
            return 0;
        }
        public int Size()
        {
            
            return size + 1; 
        }
        public int Top()
        {
            if (IsEmpty())
            {
                return -1;
            }
            return datas[size];
        }
    }
}
