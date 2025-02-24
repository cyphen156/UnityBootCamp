using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25._02._24
{
    class Sort
    {
        public void SelectionSort(ref int[] paramArray)
        {
            for (int i = 0; i < paramArray.Length; ++i)
            {
                for (int j = i+1; j < paramArray.Length; ++j)
                {
                    if (paramArray[i] - paramArray[j] > 0)
                    {
                        int temp = paramArray[i];
                        paramArray[i] = paramArray[j];
                        paramArray[j] = temp;
                    }
                }
            }
        }
    }
}
