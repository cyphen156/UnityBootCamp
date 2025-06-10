using UnityEngine;

namespace _25_06_04_AR_First.Singletons
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T instance
        {
            get
            {
                if (_instance == null)
                {
                    T component = GameObject.FindAnyObjectByType<T>();

                    if (component == null)
                    {
                        GameObject empty = new GameObject(typeof(T).Name);
                        component = empty.AddComponent<T>();
                    }

                    _instance = component;
                    DontDestroyOnLoad(_instance.gameObject);
                }
                return _instance;
            }
        }

        private static T _instance;
    }
}
