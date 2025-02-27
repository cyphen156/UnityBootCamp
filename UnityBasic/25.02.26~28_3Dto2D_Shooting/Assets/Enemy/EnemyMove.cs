using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMove : MonoBehaviour
{
    public float fireDuration;      // 발사 주기
    public GameObject target;
    public Vector3 targetPosition;  // 플레이어를 향해 발사
    public float currentTime;
    public float speed;         // 내려가는 속도
    public int levelCorrection; // 레벨 보정
    public int maxCorrection;   // 몬스터 한마리는 5번까지만 발사하셍
    public bool isFire;
    void Start()
    {
        currentTime = 0;
        levelCorrection = 1;
        maxCorrection = 5;
        fireDuration = levelCorrection;
        isFire = false;
        target = GameObject.FindGameObjectWithTag("Player");

    }

    void Update()
    {

        if (target != null)
        {
            targetPosition = target.transform.position;
        }
        // 너 나를 향해 쏴라!
        transform.
        currentTime += Time.deltaTime;

        if (currentTime >= fireDuration && isFire == false && maxCorrection > 0)
        {
            currentTime = 0;
        }
    }
}
