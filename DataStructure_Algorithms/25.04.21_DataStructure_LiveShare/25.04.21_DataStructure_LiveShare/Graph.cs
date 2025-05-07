using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25._04._21_DataStructure_LiveShare
{
    internal class Graph
    {
        int size;
        List<List<int>> graph = new List<List<int>>();

        public Graph(int inSize) 
        {
            size = inSize + 1;
            for (int i = 0; i < size; ++i)
            {
                List<int> list = new List<int>();
                graph.Add(list);
            }
        }

        public void Link(int vertex, int vertex2)
        {
            graph[vertex].Add(vertex2);
            graph[vertex2].Add(vertex); 
        }

        public int Length()
        {
            return graph.Count;
        }

        public List<int> GetVertex(int index)
        {
            return graph[index]; 
        }
    }
}
