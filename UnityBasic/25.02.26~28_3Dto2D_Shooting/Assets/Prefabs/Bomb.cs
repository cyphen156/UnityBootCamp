using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float speed;
    public float setTime;
    public float currentTime;
    public float setPosition;
    GameObject[] enemys;
    public GameObject BombEffect;
    void Start()
    {
        speed = 5f;
        setTime = 1.0f;
        currentTime = 0.0f;
        setPosition = 10f;
    }

    void Update()
    {
        currentTime += Time.deltaTime;


        transform.position += Vector3.up * speed * Time.deltaTime;

        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        
        if (currentTime > setTime)
        {
            for (int i = 0; i < enemys.Length; ++i)
            {
                Vector3 enemyPosition = enemys[i].transform.position;
                if (Vector3.Distance(enemyPosition, transform.position) < setPosition)
                {
                    Destroy(enemys[i]);
                }
            }
            Instantiate(BombEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
