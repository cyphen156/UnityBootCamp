using UnityEngine;

public class PhysicsManager : MonoBehaviour
{
    public int playerLayer;
    public int playerBulletLayer;
    public int enemyLayer;
    public int enemyBulletLayer;
    public int DZone;
    void Start()
    {
        playerLayer = 6;
        DZone = 7;
        enemyLayer = 8;
        playerBulletLayer = 9;
        enemyBulletLayer = 10;
        // 같은 그룹 내에서는 충돌하지마셍
        Physics.IgnoreLayerCollision(playerLayer, playerBulletLayer, true);
        Physics.IgnoreLayerCollision(playerBulletLayer, playerBulletLayer, true);
        Physics.IgnoreLayerCollision(enemyLayer, enemyLayer, true);
        Physics.IgnoreLayerCollision(enemyLayer, enemyBulletLayer, true);
        Physics.IgnoreLayerCollision(enemyBulletLayer, enemyBulletLayer, true);
        Physics.IgnoreLayerCollision(DZone, DZone, true);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
