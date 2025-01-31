using UnityEngine;

public class CollisionController : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject)
        {
            Destroy(collision.gameObject);
        }
    }
}

