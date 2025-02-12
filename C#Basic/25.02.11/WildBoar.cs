
namespace _25._02._11
{
    public class WildBoar : Monster
    {
        public WildBoar() 
        {
            name = "Wild Boar";
        }

        ~WildBoar() { }
        
        public void Jump()
        {
            Console.Write("Jump");
            Move();
        }
    }
}
