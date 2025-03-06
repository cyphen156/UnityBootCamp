using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject go;
    public GameObject missile;
    Vector3 firePosition;

    private void Update()
    {
        firePosition = go.transform.position;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BulletFire();
        }
    }
    void BulletFire()
    {
        Instantiate(missile, firePosition, transform.rotation);
    }
}
