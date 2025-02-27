using UnityEngine;

public class PlayerBombPool : MonoBehaviour
{
    public int maxPoolSize = 5;
    public int initCount = 3;

    GameObject[] bombObjectPool;

    private void Start()
    {
        bombObjectPool = new GameObject[maxPoolSize];

        for (int i = 0; i < initCount; ++i)
        {
            
            var bomb = bombObjectPool[i];

            
        }
    }
}
