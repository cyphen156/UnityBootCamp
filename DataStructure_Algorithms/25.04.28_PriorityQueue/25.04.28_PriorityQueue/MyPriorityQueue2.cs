namespace _25._04._28_PriorityQueue
{
    internal class MyPriorityQueue2
    {
        private List<int> _tree = new List<int>();

        /// <summary>
        /// 부모 개념 도입하기
        /// </summary>

        public void Enqueue(int _data, int value)   // value는 우선순위
        {
            // 일단 넣고
            _tree.Add(_data);

            int current = _tree.Count;
            while (current > 1)
            {
                int parent = current / 2;
                
                if (_tree[current - 1] < _tree[parent - 1])
                {
                    int temp = _tree[current - 1];
                    _tree[current - 1] = _tree[parent - 1];
                    _tree[parent - 1] = temp;

                }
                current = parent;
            }
        }

        public int Dequeue()
        {
            // 루트 뽑아내기
            int result = _tree[0];

            // 마지막 요소 루트로 가져오기
            int current = 1;
            _tree[current-1] = _tree[_tree.Count - 1];
            _tree.RemoveAt(_tree.Count - 1);

            // 트리 정렬하기
            // child = L VS R ==> L VS L+1
            // 자식이 있을 수 도 없을 수도 있다.

            while (current < _tree.Count)
            {
                int left = current * 2;
                int right = left + 1;
                int child = left;
                if (left > _tree.Count)
                {
                    break;
                }

                if (right <= _tree.Count && _tree[left - 1] > _tree[right - 1])
                {
                    child = right;
                }

                if (_tree[current - 1] <= _tree[child - 1])
                {
                    break; // 정렬 완료
                }

                int temp = _tree[current - 1];
                _tree[current - 1] = _tree[child - 1];
                _tree[child - 1] = temp;

                current = child;
            }
            return result;
        }
        public int Peek()
        {
            return _tree[0];
        }
    }
}
