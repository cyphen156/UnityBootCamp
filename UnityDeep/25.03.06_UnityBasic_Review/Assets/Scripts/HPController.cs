using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HPController : MonoBehaviour
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
        if (HP <= 0 && gameObject.tag != "Zombie")
        {
            GameObject questManager = GameObject.FindGameObjectWithTag("QuestManager");
            if (questManager != null)
            {
                questManager.GetComponent<QuestManager>().DeleteTarget();
            }
            Destroy(gameObject);
        }
        
    }


    public void OnHit(float damage, Quaternion quaternion)
    {
        Debug.Log("OnHit called");
        HP -= damage;
        if (gameObject.tag == "Zombie")
        {
            gameObject.GetComponent<NavMeshExample>().TakeDamage(damage);
        }
        Instantiate(hitEffect, transform.position, quaternion);
        
    }

    //private void OnCollisionEnter(Collision collision)
    //{
        
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    //other.GetComponent<MeshRenderer>().material.color = Color.red;
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        Debug.Log("플레이어랑 충돌함");
    //        GameObject player = other.gameObject;
    //        if (player)
    //        {
    //            //playerManager.Weapon
    //        }

    //        // 처음으로 돌아가라
    //        player.GetComponent<PlayerManager>().ResetSequence();
    //    }
    //}
}
