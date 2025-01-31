using UnityEngine;

public class FireballController : MonoBehaviour
{
    private float x, y;
    public float mF_fireballSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        x = transform.position.x;
        y = transform.position.y;
        mF_fireballSpeed = 0.3f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        y = transform.position.y;
        transform.position = new Vector3(x, y + mF_fireballSpeed, 0);
    }
}
