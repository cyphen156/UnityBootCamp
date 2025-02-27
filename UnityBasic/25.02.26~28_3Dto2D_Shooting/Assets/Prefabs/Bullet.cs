using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;

     void Start()
    {
        speed = 4f;    
    }

    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }
}
