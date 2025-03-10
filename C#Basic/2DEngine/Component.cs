using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DEngine
{
    public abstract class Component
    {
        public GameObject gameObject;
        public abstract void Update();

        public virtual void Awake() 
        {
            //gameObject
        }

        public T GetComponent<T>() where T: Component
        {
            foreach(Component component in gameObject.components)
            {
                if (component is T)
                {
                    return component as T;
                }
            }
            return null;
        }
    }
}
