using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250211
{
    public class Player
    {
        public Player()
        {
            hp = 100;
            gold = -10;
            Console.WriteLine("플레이어 생성자");
        }

        public Player(int hp, int gold)
        {
            this.hp = hp;
            this.gold = gold;
        }

        ~Player()
        {
            //Network, DB 종료
            Console.WriteLine("플레이어 소멸자");
        }

        public void Attack()
        {

        }

        public void Move()
        {
            Console.WriteLine("플레이어 뛰어다닌다.");
        }

        public void Pickup()
        {

        }

        public void Die()
        {

        }


        public int hp;
        public int gold;

    }
}
