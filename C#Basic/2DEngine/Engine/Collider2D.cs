using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DEngine
{
    class Collider2D : Component
    {
        public bool isTrigger = false;
        public override void Update()
        {
        }

        // Legacy CollisionEnter
        //public bool PredictCollision(int newX, int newY)
        //{
        //    for (int i = 0; i < Engine.Instance.world.GetAllGameObjects.Count; ++i)
        //    {
        //        if (Engine.Instance.world.GetAllGameObjects[i].GetComponent<Collider2D>() != null &&
        //                Engine.Instance.world.GetAllGameObjects[i].transform.x == newX &&
        //                Engine.Instance.world.GetAllGameObjects[i].transform.y == newY)
        //        {
        //            Type type = gameObject.GetType();
        //            Console.WriteLine(type.ToString() + "충돌");
        //            return true;
        //        }
        //    }
        //    return false;
        //}
    }
}
