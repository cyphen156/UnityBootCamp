using UnityEngine;

public class HitEffect : MonoBehaviour
{
    private float currentTime = 0f;
    private float speed = 0.5f;
    Vector3 velocity = new Vector3(0, 0, 0);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime < 0.25f)
        {
            velocity += new Vector3(0.3f, 0.6f, 0);
        }
        else if (currentTime < 0.5f)
        {
            velocity += new Vector3(0.3f, 0.6f, 0);
        }
        else if (currentTime < 1.0f)
        {
            velocity += new Vector3(0, -0.9f, 0);
        }
        else if (currentTime < 1.5f)
        {
            velocity += new Vector3(0, -0.9f, 0);
        }    
        transform.Translate(velocity * speed * Time.deltaTime);
    }
}
