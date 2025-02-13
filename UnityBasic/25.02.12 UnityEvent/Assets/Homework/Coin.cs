using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coin;
    private float rotateSpeed = 100f;

    private void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }

    public void SetCoinValue(int value)
    {
        coin = value;
    }

    public int GetCoinValue()
    {
        return coin;
    }
}
