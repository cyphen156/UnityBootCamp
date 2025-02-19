using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(Animator))]

public class Character : MonoBehaviour
{
    Animator animator;

    public double hp;
    public double attackPoint;
    protected float attackSpeed;
    protected Transform target;
    // ���� ����
    protected float attackRange;
    // Ÿ�� ����
    protected float targetRange;
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected void SetMotionChange(string motionName, bool param)
    {
        animator.SetBool(motionName, param);
    }

    protected void TargetSearch<T>(T[] targets) where T : Component
    {
        var units = targets;
        // ���� ����� �ν��Ͻ�
        Transform closet = null;
        float maxDistance = targetRange;
        foreach (var unit in units)
        {
            float distance = Vector3.Distance(transform.position, unit.transform.position);

            if (distance < maxDistance)
            {
                // ���� ������ ã�Ƽ� ��ȸŽ�����Դϴ�.
                closet = unit.transform;
                maxDistance = distance;
            }
        }
        target = closet;

        if (target != null)
        {
            //transform.LookAt(target.position);
        }
    }

    protected void Attack(Transform target)
    {

    }
}
