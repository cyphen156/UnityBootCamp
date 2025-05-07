
using static System.Net.Mime.MediaTypeNames;

namespace _25._04._01_DivideAndConquer
{
    internal class Queue
    {
        private int size;
        private int front;
        private int rear;
        
        private int[] datas;

        public Queue(int initSize)
        {
            // 입력 데이터가 없어서 큐가 비어있다는 것을 의미
            size = 0;
            front = -1;
            rear = -1;
            datas = new int[initSize];
        }

        ~Queue() { }

        public void EnQueue(int inData)
        {
            // 처음 데이터가 입력될때
            if (front == -1)
            {
                front = 0;
            }

            // 이미 인덱스가 끝에 있어 그런데? 데이터를 더 넣고 싶어
            if (size == datas.Length)
            {
                // 프론트가 0이 아니야? 넣을 공간이 있네?
                if (front != 0)
                {
                    // 처음부터 다시 넣자
                    rear = -1;
                }
                // 프론트가 0이야! 배열이 모두 찻어
                else if (rear == front-1)
                {
                    // 배열 길이 늘리기
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
                }
            }
            size++;
            rear++;
            datas[rear] = inData;
        }

        public int DeQueue()
        {
            if (IsEmpty())
            {
                return -1;
            }
            int result = datas[front++];
            // 프론트가 배열의 끝에 도달했다. -> 처음부터 다시 넣게 만들자
            if (front == datas.Length)
            {
                front = 0;
            }
            size--;
            return result;
        }

        private bool IsEmpty()
        {
            if (size == 0)
            {
                return true;
            }
            return false;
        }
        public int Empty()
        {
            if (size == 0)
            {
                return 1;
            }
            return 0;
        }
        public int Size()
        {

            return size;
        }

        public int Front()
        {
            if (IsEmpty())
            {
                return -1;
            }
            return datas[front];
        }

        public int Back()
        {
            if (IsEmpty())
            {
                return -1;
            }
            return datas[rear];
        }
    }
}
