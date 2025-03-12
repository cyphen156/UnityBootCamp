using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    public float HP = 10f;
    public GameObject hitEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnHit()
    {
        HP--;
        Instantiate(hitEffect, transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //other.GetComponent<MeshRenderer>().material.color = Color.red;
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어랑 충돌함");
            GameObject player = other.gameObject;
            if (player)
            {
                //playerManager.Weapon
            }

            // 처음으로 돌아가라
            player.GetComponent<PlayerManager>().ResetSequence();
        }
    }
}
