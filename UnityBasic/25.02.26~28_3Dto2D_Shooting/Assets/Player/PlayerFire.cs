using TMPro;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject bulletFactory;
    public GameObject bombFactory;
    public GameObject firePosition;
    public TextMeshProUGUI bombCountText;
    public int minBombCount;
    public int maxBombCount;
    public int bombCount;

    private void Start()
    {
        bombCount = 3;
        minBombCount = 0;
        maxBombCount = 5;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireBullet();
        }
        if (Input.GetKeyDown(KeyCode.G)) 
        {
            FireBomb();
        }
    }

    public void FireBullet()
    {
        GameObject bullet = Instantiate(bulletFactory, firePosition.transform.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.SetDirection(new Vector3(0, 1, 0));
            bulletScript.SetSpeed(7.0f);
        }
    }

    public void FireBomb()
    {
        if (bombCount > minBombCount)
        {
            GameObject bomb = Instantiate(bombFactory);
            bomb.transform.position = firePosition.transform.position;
            bombCount--;
            bombCountText.text = $"³²Àº ÆøÅº °¹¼ö : {bombCount}°³";
        }
    }

    public int GetBombCount()
    {
        return bombCount;
    }

    public void AddBombCount()
    {
        if (bombCount < maxBombCount)
        {
            bombCount++;
            bombCountText.text = $"³²Àº ÆøÅº °¹¼ö : {bombCount}°³";
        }
    }
}
