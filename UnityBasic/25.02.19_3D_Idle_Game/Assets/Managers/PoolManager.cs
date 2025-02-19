using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Ǯ�� ���� �۾��� �ʿ��� �������� �����ϰ� �ִ� �������̽�
// ������Ʈ�� ��� ���� �� �ִ� ������ �����
// ���� �ε� ���
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
            // ���� Ű�� ������ �߰��ϰ�
            Add(path);
        }

        if (poolDict[path].pool.Count<= 0)
        {
            AddQueue(path);
        }
        // �ִٸ� ���� ��ȯ�ض�
        return poolDict[path];
    }

    public GameObject Add(string path)
    {
        var obj = new GameObject(path + "Pool");

        ObjectPool objectPool = new ObjectPool();

        poolDict.Add(path, objectPool);

        // Ʈ������ ����
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
