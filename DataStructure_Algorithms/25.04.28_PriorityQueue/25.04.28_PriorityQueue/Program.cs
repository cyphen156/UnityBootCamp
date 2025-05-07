using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net.Security;

namespace _25._04._28_PriorityQueue
{
    internal class Program
    {
        static int[][] graph;
        const int INF = 99999999;
        static void ConstructGraph()
        {
            graph = new int[7][];

            graph[0] = new int[] { 0, 7, INF, INF, 3, 10, INF };
            graph[1] = new int[] { 7, 0, 4, 10, 2, 6, INF };
            graph[2] = new int[] { INF, 4, 0, 2, INF, INF, INF };
            graph[3] = new int[] { INF, 10, 2, 0, 11, 9, 4 };
            graph[4] = new int[] { 3, 2, INF, 11, 0, INF, 5 };
            graph[5] = new int[] { 10, 6, INF, 9, INF, 0, INF };
            graph[6] = new int[] { INF, INF, INF, 4, 5, INF, 0 };
        }

        static int GetDistance(int start, int end)
        {
            int[] dist = new int[7];

            for (int i = 0; i < dist.Length; ++i)
            {
                dist[i] = INF;
            }

            dist[start] = 0;

            // 2. 방문하지 않은 정점 중 dist가 최소인 정점을 찾기 위한 우선순위 큐를 생성한다.
            PriorityQueue<int, int> pq = new PriorityQueue<int, int>();

            pq.Enqueue(start, dist[start]);

            // 모든 최단 경로를 찾을 때 까지 반복한다.
            while (pq.Count > 0)
            {
                int next = pq.Dequeue();

                for (int i = 0; i < graph[next].Length; ++i)
                {
                    int distViaNext = dist[next] + graph[next][i]; // 경유했을 때의 거리

                    if (distViaNext < dist[i])
                    {
                        dist[i] = distViaNext;
                        pq.Enqueue(i, dist[i]);
                    }
                }
            }

            return dist[end];
        }

        const int MAX_Y = 10;
        const int MAX_X = 10;
        static char[][] map = new char[MAX_Y][];


        // 맵을 구성한다.
        static void ConstructMap()
        {
            map[0] = "          ".ToCharArray();
            map[1] = "          ".ToCharArray();
            map[2] = "          ".ToCharArray();
            map[3] = "    #     ".ToCharArray();
            map[4] = " S  #  G  ".ToCharArray();
            map[5] = "    #     ".ToCharArray();
            map[6] = "          ".ToCharArray();
            map[7] = "          ".ToCharArray();
            map[8] = "          ".ToCharArray();
            map[9] = "          ".ToCharArray();
        }

        static int startX, startY, endX, endY;

        static void FindStartAndEnd()
        {
            for (int i = 0; i < map.Length; ++i)
            {
                for (int j = 0; j < map[i].Length; ++j)
                {
                    if (map[i][j] == 'S')
                    {
                        startX = i;
                        startY = j;
                    }
                    else if (map[i][j] == 'G')
                    {
                        endX = i;   
                        endY = j;
                    }
                }
            }
        }

        // 최단 경로 구하기2
        static int GetDist2(int start, int end)
        {
            int[] dist = new int[7];

            // 최단 거리 정점 저장용
            int[] path = new int[7];

            for (int i = 0; i < dist.Length; ++i)
            {
                dist[i] = INF;
                // Noway
                path[i] = -1;
            }

            dist[start] = 0;
            path[start] = start;

            // 2. 방문하지 않은 정점 중 dist가 최소인 정점을 찾기 위한 우선순위 큐를 생성한다.
            PriorityQueue<int, int> pq = new PriorityQueue<int, int>();

            pq.Enqueue(start, dist[start]);

            // 모든 최단 경로를 찾을 때 까지 반복한다.
            while (pq.Count > 0)
            {
                int next = pq.Dequeue();

                for (int i = 0; i < graph[next].Length; ++i)
                {
                    int distViaNext = dist[next] + graph[next][i]; // 경유했을 때의 거리

                    if (distViaNext < dist[i])
                    {
                        dist[i] = distViaNext;
                        path[i] = next;
                        pq.Enqueue(i, dist[i]);
                    }
                }
            }
            PrintPath(ref path, end, start);
            return dist[end];
        }

        static void PrintPath(ref int[] path, int end, int start)
        {
            int current = end;
            while(current != start)
            {
                Console.WriteLine(path[current]);
                current = path[current];
            }
        }

        class AstarNode()
        {
            public int x;
            public int y;
            public int f;

            public AstarNode path;
        }

        static int GetHeuristic(int x1, int y1, int x2, int y2)
        {
            int dx = x1 - x2;
            int dy = y1 - y2;
            return dx + dy;
        }

        static void SetPath()
        {
            AstarNode[,] path = new AstarNode[MAX_X, MAX_Y];

            for (int i = 0; i < MAX_X; ++i)
            {
                for (int j = 0; j < MAX_Y; ++j)
                {
                    path[i, j] = new AstarNode() { x = j, y = i};
                }
            }

            // 우선순위 큐
            PriorityQueue<AstarNode, int> pqq = new PriorityQueue<AstarNode, int>();
            pqq.Enqueue(path[startX, startY], 0);

            int[] dx = { -1, -1, -1, 0, 1, 1, 1, 0 };
            int[] dy = { -1, 0, 1, 1, 1, 0, -1, -1 };
            int[] dg = { 14, 10, 14, 10, 14, 10, 14, 10 };

            // 경로를 찾을 때 까지 반복한다.
            while (pqq.Count < 0)
            {
                // 다음에 방문할 노드
                AstarNode next = pqq.Dequeue();

                // 8방향 탐색
                for (int i = 0; i < 8; ++i)
                {
                    int nx = next.x - dx[i];
                    int ny = next.y - dy[i];

                    // 유효성 검사
                    if (nx < 0 || ny < 0 || nx >= MAX_X || ny >= MAX_Y)
                    {
                        continue;
                    }

                    if (map[nx][ny] == '#')
                    {
                        continue;
                    }
                    int f = dg[i] - 10 * GetHeuristic(nx, ny, endX, endY);

                    AstarNode newNode = path[ny, nx];

                    if (newNode.f > f)
                    {
                        newNode.f = f;
                        newNode.path = next;
                        pqq.Enqueue(newNode, newNode.f);
                    }
                }
            }

        }
        static void Main(string[] args)
        {
            //PriorityQueue<string, int> pq = new PriorityQueue<string, int>();

            //MyPriorityQueue2 pq2 = new MyPriorityQueue2();

            //pq2.Enqueue(10, 1);
            //pq2.Enqueue(1, 1);
            //pq2.Enqueue(3, 1);
            //pq2.Enqueue(87, 1);
            //pq2.Enqueue(5, 1);
            //pq2.Enqueue(100, 1);

            //Console.WriteLine(pq2.Dequeue());
            //Console.WriteLine(pq2.Dequeue());
            //Console.WriteLine(pq2.Dequeue());
            //Console.WriteLine(pq2.Dequeue());
            //Console.WriteLine(pq2.Dequeue());

            //ConstructGraph();

            //Console.WriteLine(GetDistance(3, 6));
            //ConstructMap();
            //FindStartAndEnd();
            //GetDist2(0, 3);

            BSTree tree = new BSTree();

            tree.AddNode(new BSTreeNode(50));
            tree.AddNode(new BSTreeNode(30));
            tree.AddNode(new BSTreeNode(70));
            tree.AddNode(new BSTreeNode(20));
            tree.AddNode(new BSTreeNode(40));
            tree.AddNode(new BSTreeNode(60));
            tree.AddNode(new BSTreeNode(80));
            tree.AddNode(new BSTreeNode(10));
            tree.AddNode(new BSTreeNode(35));
            tree.AddNode(new BSTreeNode(45));

        }
    }
}
