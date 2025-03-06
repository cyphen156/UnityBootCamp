using UnityEngine;

public class CharactorMovement : MonoBehaviour
{
    public float moveSpeed = 4.0f;
    public float rotationSpeed = 360.0f;
    void Start()
    {
        
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        transform.Translate(Vector3.forward * v * Time.deltaTime * moveSpeed);
        transform.Rotate(Vector3.up * h * rotationSpeed);
    }
}
