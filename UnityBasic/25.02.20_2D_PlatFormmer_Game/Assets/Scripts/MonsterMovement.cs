using UnityEngine;
using UnityEngine.TestTools;

public class MonsterMovement : MonoBehaviour
{
    public float moveSpeed;
    public float flipTime;
    public float deltaTime;
    private Vector2 startPosition;
    public bool isLeft;
    // �������� = 20% ����
    public float levelcorrection;

    void Start()
    {
        levelcorrection = (float)LevelManager.level * 0.2f; // �̰� ���� ������
        flipTime = 3.0f;
        deltaTime = 0;
        startPosition = transform.position;
        moveSpeed = 2.0f + levelcorrection;
        isLeft = true;
    }
    void Update()
    {
        Move();
        UpdateDirection();
    }


    void Move()
    {
        float direction = isLeft ? 1.0f : -1.0f;
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime * direction);
    }


    void UpdateDirection()
    {
        deltaTime += Time.deltaTime;
        if (deltaTime >= flipTime)
        {
            isLeft = !isLeft; 
            Flip(); 
            deltaTime = 0f; 
        }
    }

    // ���� �̹��� ����
    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x = isLeft ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }
}
