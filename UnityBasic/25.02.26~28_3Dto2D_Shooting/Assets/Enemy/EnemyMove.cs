using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMove : MonoBehaviour
{
    public float fireDuration;      // �߻� �ֱ�
    public GameObject target;
    public Vector3 targetPosition;  // �÷��̾ ���� �߻�
    public float currentTime;
    public float speed;         // �������� �ӵ�
    public int levelCorrection; // ���� ����
    public int maxCorrection;   // ���� �Ѹ����� 5�������� �߻��ϼ�
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
        // �� ���� ���� ����!
        transform.
        currentTime += Time.deltaTime;

        if (currentTime >= fireDuration && isFire == false && maxCorrection > 0)
        {
            currentTime = 0;
        }
    }
}
