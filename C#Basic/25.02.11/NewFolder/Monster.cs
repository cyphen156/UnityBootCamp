using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250211
{
    public class Monster
    {
        public Monster()
        {
            Console.WriteLine("몬스터 생성자");
        }

        ~Monster() 
        {
            Console.WriteLine("몬스터 소멸자");
        }

        protected int hp;
        protected int gold;

        public void ApplyDamage(int damage)
        {
            if (hp < 0)
            {
                Die();
            }
            else
            {
                hp -= damage;
            }
        }

        public void Attack()
        {
            Console.WriteLine("공격한다.");
        }

        public void Die()
        {

        }

        //virtual function table
        public virtual void Move()
        {
            Console.WriteLine("몬스터 걷는다.");
        }
    }
}
