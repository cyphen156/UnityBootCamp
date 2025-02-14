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
                //          (��������ȯ)
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
            // ���� �������� T�ڷ��� ó�� ����ϰٴ�.
            // T�� �ƴϴ�
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
