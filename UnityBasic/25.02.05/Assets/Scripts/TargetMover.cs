using System;
using UnityEngine;

public class TargetMover : MonoBehaviour
{
    public float z;

    private const float border = 28;
    private float isIncrease = 1.0f;
    private float acceleration;
    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        acceleration = 0f;
        rb = GetComponent<Rigidbody>();
        Move();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        z = transform.position.z;

        // ��� ���� ���� ��ȯ
        if (Math.Abs(z) > border)
        {
            isIncrease = -isIncrease;
        }
        Move();

    }
    public void AddSpeed(float add)
    {
        acceleration += add;
        Debug.Log(acceleration);
        Move();
    }
    public void Move()
    {
        rb.linearVelocity = new Vector3(0, 0, acceleration * isIncrease);
    }
}
