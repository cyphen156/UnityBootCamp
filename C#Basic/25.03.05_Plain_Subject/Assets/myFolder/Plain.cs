using UnityEngine;

public class Plain : MonoBehaviour
{
    Vector3 position;
    float h;
    float v;
    public float speed;

    private void Awake()
    {
        position = transform.position;
        h = 0f;
        v = 0f;
        speed = 5.0f;
    }

    private void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 velocity = new Vector3(h, v, 0) * speed * Time.deltaTime;
        transform.Translate(velocity);

        Vector3 rot = new Vector3();
        if (Input.GetKey(KeyCode.Q))
        {
            rot = new Vector3(0, 0, 1);    
        }
        else if (Input.GetKey(KeyCode.E))
        {
            rot = new Vector3(0, 0, -1);
        }
        transform.Rotate(rot);
    }
}
