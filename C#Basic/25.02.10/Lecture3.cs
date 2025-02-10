namespace _25._02._10
{

    class Image
    {
        public int X;
        public int Y;

        public int Red;
        public int Green;
        public int Blue;

        public Image(int x, int y, int red, int green, int blue)
        {
            this.X = x;
            this.Y = y;
            this.Red = red;
            this.Green = green;
            this.Blue = blue;
        }
    }

    internal class Lecture3
    {
        static void Main(string[] args)
        {
            int length = 14;
            Image[] imgs = new Image[length];

            int j = 0;
            for (int i = 0; i < length; i++)
            {
                imgs[i] = new Image(j++, j++, j++, j++, j++);
            }
        }
    }
}
