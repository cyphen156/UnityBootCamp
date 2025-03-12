namespace _25._03._11_Delegate
{
    public class EventClass
    {
        public delegate void DelegateSample();

        public event DelegateSample EventSample;

        public void Do()
        {
            EventSample?.Invoke();
        }
    }
    internal class Program
    {
        public delegate int Command(int a, int b);

        public class Sample
        {
            public Command command;

            public void Sort()
            {
                if (command(1, 2) > 0)
                {

                }
            }
        }
        static int Add (int A, int B)
        {
            return A + B;
        }
        static int Sub(int A, int B)
        {
            return A - B;
        }

        public static void Test()
        {
            Console.WriteLine("TEST");
        }
        static void Main(string[] args)
        {
            Command command = Add;
            Console.WriteLine("Hello, World!");

            Sample sample = new Sample();
            sample.command += Add;
            sample.Sort();

            EventClass myEventClass = new EventClass();

            myEventClass.EventSample += Test;

            myEventClass.Do();
        }
    }
}
