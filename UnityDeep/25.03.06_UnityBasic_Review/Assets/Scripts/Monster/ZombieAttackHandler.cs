using JetBrains.Annotations;
using UnityEngine;

public class ZombieAttackHandler : MonoBehaviour
{
    public float damage = 7f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerManager>().TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }
}
