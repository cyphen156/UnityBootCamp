
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _25._02._25_2D_Engine_4.Core
{
    public class GameObject
    {
        public int x;
        public int y;
        public char shape;
        public int layer = 0;
        public bool isTrigger = true;
        public int newX = 0;
        public int newY = 0;

        private List<Component> components = new List<Component>();
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
            Engine.backBuffer[y, x] = shape;
        }

        public T AddComponent<T>() where T : Component, new()
        {
            T component = new T();
            if (component is Collider collider)
            {
                collider.Owner = this;
            }
            components.Add(component);
            return component;
        }

        public T GetComponent<T>() where T : Component
        {
            foreach (Component component in components)
            {
                if (component is T foundComponent)
                {
                    Console.WriteLine("콜라이더췤");
                    return foundComponent;
                }
            }

            return null;
        }
        public GameObject CanMove(int inX, int inY)
        {
            for (int i = 0; i < World.GetInstance().gameObjects.Count; ++i)
            {
                GameObject other = World.GetInstance().gameObjects[i];
                if (other == this)
                // 자기자신인지 췤
                {
                    continue;
                }
                if (other.x == inX && other.y == inY)
                // 좌표가 같으면 콜라이더 췍
                {
                    
                    if (other.GetComponent<Collider>() != null)
                    //콜라이더 있으면 콜리전 췤
                    {
                        if (World.GetInstance().IsCollision(this, other))
                        {
                            if (other is Monster monster)
                            {
                                monster.GameOver();
                            }
                            else if (other is Goal goal)
                            {
                                goal.NextGame();
                            }
                        }
                        // wall은 특수처리를 하지 않음
                        return null;
                    }
                    else
                    {
                        return other;
                    }
                }
            }
            return null;
        }
        public void Move(int inX, int inY)
        {
            x += inX;
            y += inY;
        }
    }
}
