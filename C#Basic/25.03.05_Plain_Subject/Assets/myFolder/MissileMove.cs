using UnityEngine;

public class MissileMove : MonoBehaviour
{
    float lifeTime;
    float speed;
    float accelSpeed;
    float deadTime;
    private void Start()
    {
        lifeTime = 0f;
        speed = 2.0f;
        accelSpeed = 0.1f;
        deadTime = 10.0f;
    }
    // Update is called once per frame
    void Update()
    {
        speed += accelSpeed;   
        lifeTime += Time.deltaTime;
        if (lifeTime > deadTime)
        {
            Destroy(gameObject);
        }
        transform.Translate(Vector3.up * speed * Time.deltaTime);


    }
}
