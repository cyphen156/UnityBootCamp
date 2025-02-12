
namespace _25._02._11
{
    public class GameObject
    {
        public string name;
        public int hp;

        public GameObject() 
        {
            name = "Object";
            hp = 100;
        }
        ~GameObject() { }
        public void Move()
        {
            Console.WriteLine(this.name + "Move");
        }

        public void Attack()
        {
            Console.WriteLine(this.name + "Attack");
        }
        public void Destroy()
        { 
            if (hp == 0)
            {
                Console.WriteLine(this.name + "Dead");
                GC.Collect();
            }
        }

    }
}
