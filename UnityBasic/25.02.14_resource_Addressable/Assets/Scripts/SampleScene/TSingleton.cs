using Unity.VisualScripting;
using UnityEngine;

public class TSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                //          (강제형변환)
                instance = (T)FindAnyObjectByType<T>();
                if (instance == null)
                {
                    var manager = new GameObject(typeof(T).Name);
                    instance = manager.AddComponent<T>();
                }
            }
            return instance;
        }
    }


    protected void Awake()
    {
        if (instance == null)
        {
            // 나는 이제부터 T자료형 처럼 취급하겟다.
            // T는 아니다
            instance = this as T;
            DontDestroyOnLoad(instance);
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            return;
        }
    }
}
