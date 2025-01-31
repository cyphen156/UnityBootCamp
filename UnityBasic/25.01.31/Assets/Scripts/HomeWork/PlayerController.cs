using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject fireball;

    private float x, y;
    private float mF_playerSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fireball = Resources.Load<GameObject>("Prefebs/fireball");

        x = transform.position.x;
        y = transform.position.y;
        mF_playerSpeed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");

        Move();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Move()
    {
        Vector3 velocity = new Vector3(x * mF_playerSpeed * Time.deltaTime, 0, 0);

        transform.position += velocity;
    }

    void Shoot()
    {
        Instantiate(fireball, new Vector3(transform.position.x, -3, 0), Quaternion.identity);
    }
}
