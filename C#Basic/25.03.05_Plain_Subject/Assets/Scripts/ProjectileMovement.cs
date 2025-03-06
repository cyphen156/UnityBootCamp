using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float moveSpeed = 10.0f;

    void Awake()
    {
        //C# ������ �÷���(�ٷ� ������ ����)
        Destroy(gameObject, 3.0f);
    }

    void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime, Space.Self);
        //transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
    }

}
