namespace _25._04._28_PriorityQueue
{
    internal class MyPriorityQueue
    {
        private List<int> _tree = new List<int>();

        /// <summary>
        /// 초기형 코드 1차원 배열로 정렬하기
        /// </summary>
        int front = 0;
        int count = 0;
        public void Enqueue(int _data, int value)   // value는 우선순위
        {
            // 일단 넣고
            _tree.Add(_data);
            count++;

            // 정렬하기
            int i = count - 1;
            for (; i > 0; --i)
            {
                // 작은놈들은 건드리지 말고 큰놈들은 하나씩 밀면서 저장하기
                if (_tree[i] < _data)
                {
                    break;
                }
                
                _tree[i] = _tree[i-1];
            }
            _tree[i] = _data;
        }
        public int Dequeue()
        {
            return _tree[front++];
        }
        public int Peek()
        {
            return _tree[front];
        }
    }
}
