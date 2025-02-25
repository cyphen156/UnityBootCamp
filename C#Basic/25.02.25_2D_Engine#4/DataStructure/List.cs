using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25._02._25_2D_Engine_4.DataStructure
{
    public class List
    {
        Node[] nodeList = new Node[10];
        int expansionCapacity = 5;
        int usedCapacity = 0;
        public List() { }
        public void Add(int inValue)
        {
            IsFull();
            nodeList[usedCapacity] = new Node();
            Console.WriteLine(usedCapacity + "에" +  inValue + "추가됨");
            nodeList[usedCapacity++].value = inValue;
        }
        public void Delete()
        {
            Console.WriteLine(usedCapacity-1 + "에서" + nodeList[usedCapacity-1].value + "삭제됨");
            nodeList[usedCapacity-1] = null;
            usedCapacity--;
        }
        // 뽑아내기
        public void Insert(int Position, int inValue)
        {
            IsFull();

            // 먼저 옮기고
            nodeList[usedCapacity] = new Node();
            int i = 0;
            for (i = usedCapacity; i > Position; i--)
            {
                // 이거 참조라서 포인터로 변환함 
                //nodeList[i] = nodeList[i - 1];
                nodeList[i].value = nodeList[i-1].value;
                Console.WriteLine(i - 1 + "에서" + (i) + "로" + nodeList[i-1].value + "옮겨짐");
            }
            nodeList[i].value = inValue;
            usedCapacity++;
            Console.WriteLine(i + "에" + nodeList[i].value + "인서트됨");
        }

        public void RemoveAt(int Position)
        {
            for (int i = Position; i < usedCapacity-1; ++i)
            {
                nodeList[i].value = nodeList[i + 1].value;
                Console.WriteLine(i + 1 + "에서" + (i) + "로" + nodeList[i + 1].value + "옮겨짐");
            }
            Console.WriteLine("RemoveAt호출에 의해 이동후 마지막 노드인");
            Delete();

        }

        public void IsFull()
        {
            if (nodeList.Length == usedCapacity)
            {
                nodeList = EnsureCapacity(nodeList);
            }
        }
        //public int find(int value)
        //{

        //}
        public int Count()
        {
            Console.WriteLine("Count 호출" + usedCapacity);
            return usedCapacity;
        }
        
        public Node[] EnsureCapacity(Node[] nodeList)
        {
            // 새로 선언했고
            Node[] newNodeList = new Node[nodeList.Length + expansionCapacity];

            // 선언 했으면 옮겨야지
            for (int i = 0; i < usedCapacity; ++i)
            {
                newNodeList[i] = nodeList[i];
            }
            Console.WriteLine("리스트 확장 :: " + newNodeList.Length);
            return newNodeList;
        }

        public void Clear()
        {
            while (usedCapacity != 0)
            {
                Delete();
            }
        }

        public void Print()
        {
            Console.WriteLine("Print() 호출");
            for (int i = 0; i < usedCapacity; ++i)
            {
                Console.WriteLine(nodeList[i].value);
            }
        }
    }
}
