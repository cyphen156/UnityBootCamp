using UnityEngine;
public abstract class AbsGameObject : MonoBehaviour, ICollideObject
{
    public GameObject dropCoinObj;

    public int hp = 10;
    public int coin = 0;

    public void OnCollisionEnter(Collision collision)
    {
        // 코인 처리 로직
        if (collision.gameObject.tag == "Coin")
        {
            Coin coinComponent = collision.gameObject.GetComponent<Coin>();
            if (coinComponent != null)
            {
                int value = coinComponent.GetCoinValue();
                GetCoin(value);
                Destroy(collision.gameObject);
            }
        }
        else
        {
            // 플레이어, 몬스터 처리 로직
            hp -= 5;
            if (hp <= 0)
            {
                DropItem();
                Destroy(gameObject);
            }
        }
    }

    public void GetCoin(int value)
    {
        coin += value;
        hp += (value/2) + 1;
    }
    protected virtual void DropItem()
    // 하드코어 임다 죽으면 템 다 떨구게 만들거임
    {
        Vector3 newPosition = transform.position + new Vector3(Random.Range(-5, 6), 3, Random.Range(-5, 6));
        if (dropCoinObj != null)
        {
            GameObject droppedItem = Instantiate(dropCoinObj, newPosition, Quaternion.identity);
            Coin coinComponent = droppedItem.GetComponent<Coin>();

            if (coinComponent != null)
            {
                coinComponent.SetCoinValue(coin);
            }
        }
    }
}
