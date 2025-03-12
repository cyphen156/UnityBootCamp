using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _2DEngine
{
    class CharacterController2D : Collider2D
    {
        public CharacterController2D ()
        {
            isTrigger = true;
        }
        public void Move(int addX, int addY)
        {
            int futureX = transform.x + addX;
            int futureY = transform.y + addY;

            for (int i = 0; i < Engine.Instance.world.GetAllGameObjects.Count; ++i)
            {
                if (Engine.Instance.world.GetAllGameObjects[i].GetComponent<Collider2D>() != null &&
                        Engine.Instance.world.GetAllGameObjects[i].transform.x == futureX &&
                        Engine.Instance.world.GetAllGameObjects[i].transform.y == futureY)
                {
                    if (Engine.Instance.world.GetAllGameObjects[i].GetComponent<Collider2D>().isTrigger == true)
                    {
                        Object[] parameters = { Engine.Instance.world.GetAllGameObjects[i].GetComponent<Collider2D>() };
                        gameObject.ExecuteMethod("OnTriggerEnter2D", parameters);
                        Object[] parameters2 = { gameObject.GetComponent<Collider2D>() };
                        Engine.Instance.world.GetAllGameObjects[i].ExecuteMethod("OnTriggerEnter2D", parameters2);
                        break;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            transform.TransLate(addX, addY);
        }

        
    }
}
