using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// 풀에 대한 작업시 필요한 정보들을 보관하고 있는 인터페이스
// 오브젝트를 담고 있을 수 있는 일종의 저장소
// 사전 로딩 방식
public interface IPool
{
    //property
    Transform parent { get; set; }
    Queue<GameObject> pool { get; set; }

    //fnc
    GameObject GetGameObject(Action<GameObject> action = null);

    void ObjectReturn(GameObject ob, Action<GameObject> action = null);

}

public class ObjectPool : IPool
{
    public Transform parent { get; set; }
    public Queue<GameObject> pool { get; set; } = new Queue<GameObject>();

    public GameObject GetGameObject(Action<GameObject> action = null)
    {
        var obj = pool.Dequeue();
        obj.SetActive(true);

        if (action != null)
        {
            action.Invoke(obj);
        }
        return obj;
    }

    public void ObjectReturn(GameObject ob, Action<GameObject> action = null)
    {
        pool.Enqueue(ob);
        ob.transform.parent = parent;
        ob.SetActive(false);

        if (action != null)
        {
            action.Invoke(ob);
        }
    }
}

public class PoolManager
{
    public Dictionary<string, IPool> poolDict = new Dictionary<string, IPool>();

    public IPool PoolObjcet(string path)
    {
        if (!poolDict.ContainsKey(path))
        {
            // 만약 키가 없으면 추가하고
            Add(path);
        }

        if (poolDict[path].pool.Count<= 0)
        {
            AddQueue(path);
        }
        // 있다면 값을 반환해라
        return poolDict[path];
    }

    public GameObject Add(string path)
    {
        var obj = new GameObject(path + "Pool");

        ObjectPool objectPool = new ObjectPool();

        poolDict.Add(path, objectPool);

        // 트랜스폼 설정
        objectPool.parent = obj.transform;
        
        return obj;
    }


    public void AddQueue(string path)
    {
        var gO = BaseManager.instance.CreateFromPath(path);
        gO.transform.parent = poolDict[path].parent;

        poolDict[path].ObjectReturn(gO);
    }
}
