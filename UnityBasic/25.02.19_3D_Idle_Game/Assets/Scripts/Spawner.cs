using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // ���� ������ ����
    [SerializeField] private int spawnMaxCount;
    [SerializeField] private int spawnedMonster;
    [SerializeField] private int spawnMonsterCount;

    // ���� ���� �ֱ�
    [SerializeField] private int spawnCycle;

    // ���� ���� ��ġ ����
    [SerializeField] private float maxWorldSize;

    public GameObject monsterPrefab;


    public static List<Monster> monsterList = new List<Monster>();  // ������ ����
    public static List<Player> playerList = new List<Player>();     // ������ ĳ����

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
            // ������ġ �ٲٱ�
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
            // ������ġ �ٲٱ�
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
            // ���ٽ����� ��������Ʈ �����Լ� ���ڰ� �������� ������ ���� ����.

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
