using JetBrains.Annotations;
using UnityEngine;

public class ZombieAttackHandler : MonoBehaviour
{
    public LayerMask playerLayer = 3;
    
    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            gameObject.SetActive(false);
        }
    }
}
