using System.Data;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float currentTime;
    public float deadTime;
    public GameObject ExplosionEffect;
    private void Start()
    {
        currentTime = 0.0f;
        deadTime = 3.0f;
    }
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != 7)
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.layer == 9)
        {
            Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
