namespace _25._02._10
{


    class World
    {
        int x;
        int y;
        public bool canPass;

        public World(int wall, int floor)
        {
        }
    }

    class Object
    {
        public int x = 0; 
        public int y = 0;

        void Move()
        {

        }

        void Move(int to)
        {

        }

        public Object()
        { 
        
        }
    }
    internal class EscapeRoom
    {
        static void Main(string[] args)
        {
            World[] Walls = new World[10];
            World[] floor = new World[10];

            Object player = new Object();
            Object[] monsters = new Object[3];

            int DestinationX, DestinationY;

            if (player.x == DestinationX && player.y == DestinationY)
            {
                Console.WriteLine("Goal");
            }
        }
    }
}
