using Unity.Mathematics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    /// <summary>
    /// ���� �޾ƾ� �� ����
    /// ���⺤��
    /// �ӵ�
    /// ����
    /// </summary>
    public float speed;
    public Vector3 direction;
    public int ownerLayer;

    public GameObject ExplosionEffect;

    void Start()
    {
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
    public void SetDirection(Vector3 inDirection)
    {
        direction = inDirection;
    }
    public void SetSpeed(float inSpeed)
    {
        speed = inSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != 7)
        {
            Destroy(collision.gameObject);
            Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
