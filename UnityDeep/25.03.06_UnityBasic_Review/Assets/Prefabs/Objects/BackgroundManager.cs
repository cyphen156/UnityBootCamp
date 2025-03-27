using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    public Sprite img;
    public Sprite img2;
    public float currentTime = 0f;

    public Image currentImg;
    public bool isChanged = false;
    public float setTime = 5f;
    private void Awake()
    {
        currentImg.sprite = img;
    }
    void Update()
    {
        currentTime += Time.deltaTime;
        
        if (currentTime > setTime)
        {
            currentTime = 0f;
            isChanged = !isChanged;
            if (isChanged)
            {
                currentImg.sprite = img2;
            }

            else
            {
                currentImg.sprite = img;
            }
        }
    }
}
