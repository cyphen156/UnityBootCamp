
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25._02._24.Core
{
    public class GameObject
    {
        public int x;
        public int y;
        public char shape;
        public int layer = 0;
        public bool isTrigger = true;


        private List<object> components = new List<object>();
        //public GameObject() { }
        public GameObject (int inX, int inY, char inShape)
        {
            x = inX;
            y = inY;
            shape = inShape;
        }
        public virtual void Update()
        {
            
        }
        public void Render()
        {
            Console.SetCursorPosition(x, y);
            Console.Write(shape);
        }

        public T AddComponent<T>() where T : new()
        {
            T component = new T();
            if (component is Collider collider)
            {
                collider.Owner = this;
            }
            components.Add(component);
            return component;
        }

        public T GetComponent<T>() where T : class 
        {
            foreach (object component in components)
            {
                if (component is T foundComponent)
                {
                    return foundComponent;
                }
            }

            return null;
        }
    }
}
