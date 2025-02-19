using System;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    public static BaseManager instance;
    private static PoolManager poolManager = new PoolManager();
    public static PoolManager POOL
    {
        get
        {
            return poolManager;
        }
    }
    private void Awake()
    {
        Initalize();
    }

    private void Initalize()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject CreateFromPath(string path)
    {
        return Instantiate(Resources.Load<GameObject>(path));
    }
}
