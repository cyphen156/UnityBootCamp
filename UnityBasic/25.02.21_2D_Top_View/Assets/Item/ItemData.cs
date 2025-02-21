using UnityEngine;


public enum ItemType
{
    arrow,
    key,
    life
}
public class ItemData : MonoBehaviour
{
    public ItemType type;

    public const int count = 1;
    public int arrageId = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            switch (type)
            {
                case ItemType.arrow:
                {
                    ArrowShoot shoot = collision.gameObject.GetComponent<ArrowShoot>();
                    ItemKeeper.hasArrows += count;
                    break;
                }
                case ItemType.key:
                    ItemKeeper.hasKey += count;
                    break;
                case ItemType.life:
                    if (PlayerController.hp < 3)
                    {
                        PlayerController.hp++;
                    }
                    break;
            }

            GetComponent<CircleCollider2D>().enabled = false;

            var itemRb = GetComponent<Rigidbody2D>();

            itemRb.gravityScale = 2.5f;
            itemRb.AddForce(new Vector2(0, 6), ForceMode2D.Impulse);

            Destroy(gameObject, 0.5f);
        }
    }
}
