using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject bulletFactory;
    public GameObject firePosition;

    public void FireBullet()
    {
        GameObject bullet = Instantiate(bulletFactory);
        bullet.transform.position = firePosition.transform.position;
    }
}
