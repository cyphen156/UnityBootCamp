using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace _25._02._25_2D_Engine_4.DataStructure
{
    class DynamicArray<T>
    {
        public int arraySize = 10;
        private int expansion = 5;
        public uint usedSize = 0;
        public T[] dataList;

        public DynamicArray()
        {
            dataList = new T[arraySize];
        }

        ~DynamicArray() { }

        private bool CheckAllocation()
        {
            if (usedSize == arraySize)
            {
                return true;
            }
            return false;
        }

        private T[] ReAllocation(T[] origin)
        {
            arraySize += expansion;
            T[] newdataList = new T[arraySize];
            for (int i = 0; i < usedSize; ++i)
            {
                newdataList[i] = origin[i];
            }
            Console.WriteLine("after ReAllocation() Called :: " + arraySize);
            return newdataList;
        }

        public void Add(T inData)
        {
            if (CheckAllocation())
            {
                dataList = ReAllocation(dataList);
            }
            dataList[usedSize++] = inData;
        }

        public void RemoveAt(int index)
        {
            // 자료의 접근은 []연산자로 되야 하니까 인덱스로 접근
            Console.WriteLine("RemoveAt(" + index.ToString() + ") :: " + dataList[index]);
            for(int i = index; i < usedSize; ++i)
            {
                dataList[i] = dataList[i + 1];
            }
            usedSize--;
        }

        public void Count()
        {
            Console.WriteLine("Count :: " + usedSize.ToString());
        }

        public void PrintSize()

        {
            Console.WriteLine(arraySize);
        }
    }
}
