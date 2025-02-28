using System.Collections;
using System.Data;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyController : MonoBehaviour
{
    public float fireDuration;      // 발사 주기
    public GameObject target;
    public Vector3 targetPosition;  // 플레이어를 향해 발사
    public Vector3 direction;
    public Quaternion targetRotation;
    public float rotateZ;
    public float levelCorrection; // 레벨 보정
    public float fireCount;
    public float maxDist;
    public float maxCorrection;   // 몬스터 한마리는 5번까지만 발사하셍
    public bool isFire;
    public GameObject bulletFactory;
    public GameObject firePosition;

    private void Start()
    {
        levelCorrection = LevelManager.Instance.GetLevel();
        maxCorrection = 5;
        fireDuration = levelCorrection;
        maxDist = 15f;
        isFire = false;
        target = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (target != null)
        {
            targetPosition = target.transform.position;
            // 너 나를 향해 봐라!
            direction = targetPosition - transform.position;
            float angle = Vector3.SignedAngle(new Vector3(1, 0, 0), direction, new Vector3(0, 0, 1));

            targetRotation = Quaternion.Euler(0, 0, angle - 90);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 180f);
        }
        if (Vector3.Distance(transform.position, targetPosition) <= maxDist && !isFire)
        {
            isFire = true;
            StartCoroutine(Fire());
        }
    }
    
    IEnumerator Fire()
    {
        fireCount = levelCorrection;
        if (fireCount > maxCorrection)
        {
            fireCount = maxCorrection;
        }
        // 0.2초에 한번씩 5번까지 쏴라
        for (int i = 0; i < fireCount; ++i)
        {
            GameObject bullet = Instantiate(bulletFactory, firePosition.transform.position, Quaternion.identity);
            Bullet bulletScript = bullet.GetComponent<Bullet>();

            if (bulletScript != null)
            {
                bulletScript.SetDirection(direction.normalized);
                bulletScript.SetSpeed(4.0f);
            }
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(2f);
        isFire = false;
    }
}
