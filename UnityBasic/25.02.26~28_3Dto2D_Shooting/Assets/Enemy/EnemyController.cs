using System.Collections;
using System.Data;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyController : MonoBehaviour
{
    public float fireDuration;      // �߻� �ֱ�
    public GameObject target;
    public Vector3 targetPosition;  // �÷��̾ ���� �߻�
    public Vector3 direction;
    public Quaternion targetRotation;
    public float rotateZ;
    public float levelCorrection; // ���� ����
    public float fireCount;
    public float maxDist;
    public float maxCorrection;   // ���� �Ѹ����� 5�������� �߻��ϼ�
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
            // �� ���� ���� ����!
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
        // 0.2�ʿ� �ѹ��� 5������ ����
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
