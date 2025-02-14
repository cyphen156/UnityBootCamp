using UnityEngine;

public class GenericSample : MonoBehaviour
{
    public static void PrintArray<T>(T[] t)
    {
        foreach (var i in t)
        {
            Debug.Log(i);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
