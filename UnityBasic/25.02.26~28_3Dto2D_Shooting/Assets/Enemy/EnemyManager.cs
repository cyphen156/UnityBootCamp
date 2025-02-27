using System;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float currentTime;
    public float createTime;    // 생성 주기
    public GameObject enemyFactory;

    public float x;
    public float y;
    void Start()
    {
        createTime = 1.0f;
        x = transform.position.x;
        y = transform.position.y;
        gameObject.SetActive(false);
    }

     void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > createTime)
        {
            currentTime = 0.0f;
            CreateEnemy();
        }
    }

    private void CreateEnemy()
    {
        x = UnityEngine.Random.Range(-3, 4);
        
        Instantiate(enemyFactory, new Vector3(x, y, 0), Quaternion.Euler(0, 0, 180));
    }
}
