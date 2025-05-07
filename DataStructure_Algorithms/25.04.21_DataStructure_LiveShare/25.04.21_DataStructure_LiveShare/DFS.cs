using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25._04._21_DataStructure_LiveShare
{
    class DFS
    {
        static bool[] visited;
        static Graph graph;

        public static int Search(Graph g, int vertex) 
        {
            graph = g;
            visited = new bool[g.Length()];
            
            return Visit(vertex) - 1;
        }

        private static int Visit(int vertex)
        {
            // 아니면 연결된 모든 링크 탐색
            visited[vertex] = true;

            int count = 1;

            List<int> newVertex = graph.GetVertex(vertex);

            foreach (var linkedVertex in newVertex)
            {
                if (visited[linkedVertex] == false)
                {
                    count += Visit(linkedVertex);
                }
            }

            return count;
        }
    }
}
