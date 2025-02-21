using System;
using UnityEngine;

public class ArrowShoot : MonoBehaviour
{
    public float speed = 12.0f;
    public float delay = 0.25f;
    public GameObject BowPrefab;
    public GameObject ArrowPrefab;

    bool isAttack;
    GameObject bowObject;

    PlayerController playerController;

    void Start()
    {
        Vector3 pos = transform.position;
        bowObject = Instantiate(BowPrefab, pos, Quaternion.identity); ;
        bowObject.transform.SetParent(transform);
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            Attack();
        }

        float bowZ = -1;

        playerController = GetComponent<PlayerController>();

        if (playerController.z > 30 && playerController.z < 150)
        {
            bowZ = 1;
        }

        bowObject.transform.rotation = Quaternion.Euler(0, 0, playerController.z);
        bowObject.transform.position = new Vector3(transform.position.x, transform.position.y, bowZ);
    }

    private void Attack()
    {
        if (ItemKeeper.hasArrows > 0 && isAttack == false)
        {
            ItemKeeper.hasArrows--;
            isAttack = true;
            playerController = GetComponent<PlayerController>();

            float z = playerController.z;

            var rotation = Quaternion.Euler(0, 0, z);

            var ArrowObject = Instantiate(ArrowPrefab, transform.position, rotation);

            float x = MathF.Cos(z * Mathf.Deg2Rad);
            float y = MathF.Sin(z * Mathf.Deg2Rad);


            Vector3 vec3 = new Vector3(x, y) * speed;

            var rb = ArrowObject.GetComponent<Rigidbody2D>();

            rb.AddForce(vec3, ForceMode2D.Impulse);

            Invoke("AttackChange", delay);
        }
    }
    public void AttackChange() => isAttack = false;

}
