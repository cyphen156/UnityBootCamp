using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // 몬스터 마릿수 제한
    [SerializeField] private int spawnMaxCount;
    [SerializeField] private int spawnedMonster;
    [SerializeField] private int spawnMonsterCount;

    // 몬스터 스폰 주기
    [SerializeField] private int spawnCycle;

    // 몬스터 스폰 위치 제한
    [SerializeField] private float maxWorldSize;

    public GameObject monsterPrefab;


    public static List<Monster> monsterList = new List<Monster>();  // 생성된 몬스터
    public static List<Player> playerList = new List<Player>();     // 생성된 캐릭터

    public void Start()
    {
        spawnMaxCount = 100;
        spawnMonsterCount = 3;
        spawnedMonster = 0;
        maxWorldSize = 50;
        //StartCoroutine(SpawnMonster());
        StartCoroutine(SpawnMonsterPooling());
    }

    IEnumerator SpawnMonster()
    {
        
        for (int i = 0; i < spawnMonsterCount; ++i)
        {
            // 생성위치 바꾸기
            Vector3 spqwnPosition = new Vector3(
            UnityEngine.Random.Range(-maxWorldSize+1, maxWorldSize)
            , 0
            , UnityEngine.Random.Range(-maxWorldSize+1, maxWorldSize));

            if (spawnedMonster >= spawnMaxCount)
            {
                break;
            }
            Instantiate(monsterPrefab, spqwnPosition, quaternion.identity);;
            spawnedMonster++;
            yield return new WaitForSeconds(0.2f);

        }
        yield return new WaitForSeconds(spawnMonsterCount);
        StartCoroutine(SpawnMonster());
    }

    IEnumerator SpawnMonsterPooling()
    {

        for (int i = 0; i < spawnMonsterCount; ++i)
        {
            // 생성위치 바꾸기
            Vector3 spawnPosition = new Vector3(
            UnityEngine.Random.Range(-maxWorldSize + 1, maxWorldSize)
            , 0
            , UnityEngine.Random.Range(-maxWorldSize + 1, maxWorldSize));

            if (spawnedMonster >= spawnMaxCount)
            {
                break;
            }
            //Instantiate(monsterPrefab, spawnPosition, quaternion.identity);
            //var go = BaseManager.POOL.PoolObjcet("Slime").GetGameObject();
            // 람다식으로 델리게이트 전달함수 인자가 있을때는 다음과 같이 쓴다.

            var go = BaseManager.POOL.PoolObjcet("Slime").GetGameObject((result) =>
            {
                result.GetComponent<Monster>();
                result.transform.position = spawnPosition;
                monsterList.Add(result.GetComponent<Monster>());
            });

            spawnedMonster++;
            //StartCoroutine(ReturnMonsterPooling(go));
            yield return new WaitForSeconds(0.2f);

        }
        yield return new WaitForSeconds(spawnMonsterCount);
        StartCoroutine(SpawnMonsterPooling());
    }

    IEnumerator ReturnMonsterPooling(GameObject gO)
    {
        yield return new WaitForSeconds(1.0f);
        BaseManager.POOL.poolDict["Slime"].ObjectReturn(gO);
    }
}
