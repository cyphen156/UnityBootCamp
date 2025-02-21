using UnityEngine;

public class ArrowController : MonoBehaviour
{
    float remove = 2.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, remove);
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.SetParent(collision.transform);

        GetComponent<CircleCollider2D>().enabled = false;

        GetComponent<Rigidbody2D>().simulated = false;
    }
}
