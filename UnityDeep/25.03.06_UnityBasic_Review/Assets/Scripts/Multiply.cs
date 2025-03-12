using UnityEngine;

public class Multiply : MonoBehaviour
{
    int end;
    int[,] array;
    float currentTime;
    int iIndex;
    int jIndex;
    float setTime;
    private void Awake()
    {
        array = new int[9, 9];
        currentTime = 0f;
        iIndex = 0;
        jIndex = 0;
        setTime = 1f;
    }
    void Start()
    {
        end = 9;
        for (int i = 0; i < end; ++i)
        {
            for (int j = 0; j < end; ++j)
            {
                array[i, j] = (i + 1) * (j + 1);
            }
        }
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > setTime && iIndex < 9)
        {
            currentTime = 0f;
            Debug.Log((iIndex + 1).ToString()
                + " * "
                + (jIndex + 1).ToString()
                + " = "
                + array[iIndex, jIndex].ToString());
            jIndex++;

            if (jIndex >= 9)
            {
                iIndex++;
                jIndex = 0;
            }
        }
    }
}
