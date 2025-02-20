using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;


// ������ �Ҵ�Ǵ� ����, ��ֹ� ������
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
                    // ���� ���� (���ݸ� ����)
                    newObject = Instantiate(monsterpreFab, new Vector2(randX, randY), Quaternion.identity);
                    gameObjects.Add(newObject);
                }
                else
                {
                    newObject = Instantiate(needle, new Vector2(randX2, randY), Quaternion.identity);
                    gameObjects.Add(newObject);
                }

                // ������ ������Ʈ�� ���� ������Ʈ��� �浹���� �ʵ��� ����
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
            Debug.LogError("gameObjects ����Ʈ�� null�Դϴ�. LevelManager.Start()�� ȣ��Ǿ����� Ȯ���ϼ���.");
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

        // ���� gameObjects ����Ʈ�� �ִ� ������Ʈ��� �浹 ����
        foreach (GameObject go in gameObjects)
        {
            if (go != null && go != newObject) // �ڱ� �ڽŰ� ������ �ʵ���
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
