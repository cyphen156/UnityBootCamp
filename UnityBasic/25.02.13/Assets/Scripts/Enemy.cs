using UnityEngine;

public class Enemy : MonoBehaviour
{
    public DropTable DropTable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Dead();
        }
    }
    void Dead()
    {
        GameObject dropItemPrefab = DropTable.drop_Table[Random.Range(0, DropTable.drop_Table.Count)];
        Instantiate(dropItemPrefab);
        //Destroy(gameObject);
    }
}
