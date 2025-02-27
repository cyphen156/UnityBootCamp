using UnityEngine;

public class BombEffect : MonoBehaviour
{
    private float setTime;
    private float currentTime;
    private void Awake()
    {
        currentTime = 0.0f;
        setTime = 2.0f;
    }
    private void Update()
    {
        if (currentTime > setTime)
        {
            Destroy(gameObject);
        }
    }
}
