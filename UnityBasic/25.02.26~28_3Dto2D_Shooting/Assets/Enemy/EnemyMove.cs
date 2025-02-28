using UnityEngine;
using Unity.Mathematics;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class EnemyMove : MonoBehaviour
{
    public float speed;         // 내려가는 속도
    void Start()
    {
        speed = 3.0f;
    }

    void Update()
    {
        // 항상 아래로만 진행함
        transform.position += new Vector3(0, -1, 0) * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != 7)
        {
            ScoreManager.Instance.AddScore(5);

            if (collision.gameObject.CompareTag("Player"))
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }        
    }
}
