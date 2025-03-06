using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float moveSpeed = 10.0f;

    void Awake()
    {
        //C# 가비지 컬렉터(바로 지우지 않음)
        Destroy(gameObject, 3.0f);
    }

    void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime, Space.Self);
        //transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
    }

}
