
namespace _25._02._11
{
    public class Slime : Monster
    {
        public Slime()
        {
            name = "Slime";
        }

        ~Slime() { }

        void Slide()
        {
            Console.Write("Slide");
            Move();
        }

    }
}
