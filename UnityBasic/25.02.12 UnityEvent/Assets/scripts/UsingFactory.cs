using Unity.VisualScripting;
using UnityEngine;

public class UsingFactory : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnemyFactory enemyFactory = new EnemyFactory();
        Enemy enemy = enemyFactory.Create(EnemyFactory.EnemyType.Goblin);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
