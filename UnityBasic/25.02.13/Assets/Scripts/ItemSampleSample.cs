using System;
using UnityEngine;

public class ItemSampleSample : MonoBehaviour
{
    public ItemSample itemSample;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ItemInfo();
        }
    }
    private void ItemInfo()
    {
        Debug.Log(itemSample.name);
        Debug.Log(itemSample.ID);
        Debug.Log(itemSample.price);
        Debug.Log(itemSample.description);

    }
}
