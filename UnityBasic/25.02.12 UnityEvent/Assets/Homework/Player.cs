using UnityEngine;

public class Player : AbsGameObject
{
    public float moveSpeed = 5f;

    private void Start()
    {
        hp = 100;
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 vec3 = new Vector3(x, 0, z).normalized;
        transform.position += vec3 * moveSpeed * Time.deltaTime;

        // 이동 기능 구현
    }
}
