
namespace _25._02._11
{
    public class Player : GameObject
    {
        public int gold = 0;

        public void Haunt()
        {
            Attack();
            gold++;
        }
    }
}
