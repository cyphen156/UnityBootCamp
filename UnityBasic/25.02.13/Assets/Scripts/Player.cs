using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public AudioEvent audioEvent;
    public ItemSample itemSample;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioEvent.OnPlay += PlaySound;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            audioEvent.Play("등장 배경음");
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            DropItem();
        }
    }

    private void DropItem()
    {
        GameObject item = itemSample.gObj;

        Instantiate(item, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    public void PlaySound(string soundName)
    {
        Debug.Log(soundName + "플레이 중입니다.");
    }
}
