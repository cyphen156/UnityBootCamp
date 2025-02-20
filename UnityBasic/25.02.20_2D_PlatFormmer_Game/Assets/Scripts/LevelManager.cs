using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;


// 레벨에 할당되는 몬스터, 장애물 생성기
public class LevelManager : MonoBehaviour
{
    public static int level = 0;
    public List<GameObject> gameObjects;
    public GameObject monsterpreFab;
    public GameObject needle;
    private float minPos;
    private float maxPos;
    private void Start()
    {
        gameObjects = new List<GameObject>();
        minPos = -5f;
        maxPos = 26f;
        GenerateEnemy();
    }

    public void GenerateEnemy()
    {

        if (level != 0)
        {
            for (int i = 0; i < level; ++i)
            {
                float randX = Random.Range(minPos, maxPos);
                float randX2 = Random.Range(minPos, maxPos);
                float randY = Random.Range(0, 5);

                GameObject newObject;

                if (i % 2 != 0)
                {
                    // 몬스터 생성 (절반만 생성)
                    newObject = Instantiate(monsterpreFab, new Vector2(randX, randY), Quaternion.identity);
                    gameObjects.Add(newObject);
                }
                else
                {
                    newObject = Instantiate(needle, new Vector2(randX2, randY), Quaternion.identity);
                    gameObjects.Add(newObject);
                }

                // 생성된 오브젝트가 기존 오브젝트들과 충돌하지 않도록 설정
                IgnoreCollisions(newObject);
            }
        }
        
    }

    public void StageUP()
    {
        level++;
    }

    public void LevelReset()
    {
        if (gameObjects == null)
        {
            Debug.LogError("gameObjects 리스트가 null입니다. LevelManager.Start()가 호출되었는지 확인하세요.");
            return;
        }
        foreach (GameObject go in gameObjects)
        {
            Destroy(go);
        }
        gameObjects.Clear();
    }

    void IgnoreCollisions(GameObject newObject)
    {
        Collider2D newCollider = newObject.GetComponent<Collider2D>();

        // 기존 gameObjects 리스트에 있는 오브젝트들과 충돌 무시
        foreach (GameObject go in gameObjects)
        {
            if (go != null && go != newObject) // 자기 자신과 비교하지 않도록
            {
                Collider2D otherCollider = go.GetComponent<Collider2D>();
                if (otherCollider != null)
                {
                    Physics2D.IgnoreCollision(newCollider, otherCollider, true);
                }
            }
        }
    }

}
