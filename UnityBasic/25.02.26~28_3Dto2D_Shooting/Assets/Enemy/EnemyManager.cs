using System;
using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float currentTime;
    public float createDelay;    // 생성 주기
    public GameObject enemyFactory;
    public bool isCreating;
    public float x;
    public float y;

    private float levelCorrection; // 레벨 보정 && 몬스터 생성은 제한없이 갈거임

    void Start()
    {
        levelCorrection = 0;
        createDelay = 3.0f;  // 한번 생성된 후 딜레이는 3초
        x = transform.position.x;
        y = transform.position.y;
        isCreating = false;
        gameObject.SetActive(false);
    }

     void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > createDelay && !isCreating)
        {
            isCreating = true;
            StartCoroutine(CreateEnemy());
        }
    }

    IEnumerator CreateEnemy()
    {
        levelCorrection = LevelManager.Instance.GetLevel();

        for (int i = 0; i < levelCorrection; ++i)
        {
            x = UnityEngine.Random.Range(-15, 15);
            Instantiate(enemyFactory, new Vector3(x, y, 0), Quaternion.Euler(0, 0, 180));
            yield return new WaitForSeconds(0.2f);
        }
        currentTime = 0.0f;
        isCreating = false;
    }
}
