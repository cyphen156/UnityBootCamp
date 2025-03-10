using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DEngine
{
    class Collider : Component
    {
        public override void Update()
        {
        }
        public bool PredictCollision(int newX, int newY)
        {
            for (int i = 0; i < Engine.Instance.world.GetAllGameObjects.Count; ++i)
            {
                if (Engine.Instance.world.GetAllGameObjects[i].isCollide == true &&
                        Engine.Instance.world.GetAllGameObjects[i].transform.x == newX &&
                        Engine.Instance.world.GetAllGameObjects[i].transform.y == newY)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
