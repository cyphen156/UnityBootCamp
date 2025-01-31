using System;
using UnityEngine;

public class CreateObject2 : MonoBehaviour
{
    public GameObject prefeb;

    private GameObject gameObject2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject2 = Instantiate(prefeb);

        Destroy(gameObject2, 3.0f);
        Debug.Log("Destroy Object2 :: prefeb");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
