using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public interface ICountable
{
    public int Count
    {
        get;
        set;
    }

    void CountPlus();
}

public interface IUseable
{
    public int Usable();
}

public class Item
{

}
class Position : Item, ICountable, IUseable
{
    private int count;
    private string name;
    public int Count
    {
        get
        {
            return Count;
        }
        set
        {
            if (Count > 99)
            {
                Debug.Log("count는 99가 최대임");
                Count = 99;
            }
            Count = value;
        }
    }

    public string Name { get => name; set => name = value; }

    public void CountPlus()
    {
        Count++;
    }

    public void Use()
    {
        Debug.Log($"{Name}을 사용");
        Count--;
    }
    public int Usable()
    {
        return Count;
    }
}

public class interfaceSample : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class Class1
{
}