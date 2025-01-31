using System.Collections;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DestroySelf());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DestroySelf()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            if (gameObject)
            {
                Destroy(gameObject);
            }
        }
    }
}
