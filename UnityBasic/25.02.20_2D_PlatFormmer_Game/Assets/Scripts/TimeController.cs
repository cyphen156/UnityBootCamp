using UnityEngine;

public class TimeController : MonoBehaviour
{
    public bool isCountDown;
    public float gameTime;
    public bool isTimeOver;
    public float displayTime;

    public float times;

    void Start()
    {
        isCountDown = true;
        gameTime = 60;
        isTimeOver = false;
        displayTime = 60;
        times = 0;
        if (isCountDown)
        {
            displayTime = gameTime;
        }
    }

    void Update()
    {
        if (!isTimeOver)
        {
            times = Time.deltaTime;

            if (isCountDown)
            {
                displayTime = gameTime -= times;
            }

            if (displayTime <= 0f)
            {
                displayTime = 0f;
                isTimeOver = true;
            }
            else
            {
                if(displayTime >= gameTime)
                {
                    displayTime = gameTime;
                    //isTimeOver = true;
                }
            }
        }
    }
}
