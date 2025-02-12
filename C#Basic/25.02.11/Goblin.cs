
namespace _25._02._11
{
    public class Goblin : Monster
    {
        Goblin() 
        {
            name = "Goblin";
        }
        ~Goblin() { }

        public void Walk()
        {
            Console.Write("Walk");
            Move();
        }
    }
}
