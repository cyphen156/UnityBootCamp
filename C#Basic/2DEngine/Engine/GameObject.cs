using SDL2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static SDL2.SDL;

namespace _2DEngine
{
    public class GameObject
    {
        /*
        public int X;
        public int Y;
        public int orderLayer;
        public char Shape; //Mesh, Sprite
        public SDL.SDL_Color color;
        public int spriteSize = 15;
        protected IntPtr myTexture;
        protected IntPtr mySurface;
        protected bool isAnimation = false;
        protected int spriteIndexX = 0;
        protected int spriteIndexY = 0;

        protected float elapsedTime = 0;
        protected SDL.SDL_Color colorKey;


         */
        public List<Component> components = new List<Component>();

        public bool isTrigger = false;
        public bool isCollide = false;

        public string name;

        protected static int gameObjectCount = 0;

        public Transform transform;

        public GameObject()
        {
            Init();
            gameObjectCount++;
            name = $"GameObject({gameObjectCount})";
        }

        public void Init()
        {
            transform = AddComponent<Transform>(new Transform());
        }

        public T AddComponent<T>(T inComponent) where T : Component
        {
            components.Add(inComponent);
            inComponent.gameObject = this;
            inComponent.transform = transform;
            return inComponent;
        }
        public T AddComponent<T>() where T : Component, new()
        {
            T t = new T();
            components.Add(t);
            t.gameObject = this;
            t.transform = transform;
            return t;
        }

        //public T RemoveComponent<T> (T component) where T : Component
        //{
        //    return component;
        //}

        //public T FindComponent<T>(T component) where T : Component
        //{
        //    return component;
        //}
        public T GetComponent<T>() where T : Component
        {
            foreach (Component component in components)
            {
                if (component is T)
                {
                    return component as T;
                }
            }
            return null;
        }
        public virtual void Update()
        {

        }

        public void ExecuteMethod(string methodName, Object[] parameters)
        {
            foreach (var component in components)
            {
                Type type = component.GetType();
                MethodInfo[] methodInfos = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (var methodInfo in methodInfos)
                {
                    if (methodInfo.Name.CompareTo(methodName) == 0)
                    {
                        methodInfo.Invoke(component, parameters);
                    }
                }
            }
        }

        public static GameObject Find(string gameObjectName)
        {
            foreach (var choiceObject in Engine.Instance.world.GetAllGameObjects)
            {
                if (choiceObject.name.CompareTo(gameObjectName) == 0)
                {
                    return choiceObject;
                }
            }

            return null;
        }
    }
}