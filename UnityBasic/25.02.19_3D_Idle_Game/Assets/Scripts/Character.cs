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
    // 공격 범위
    protected float attackRange;
    // 타겟 범위
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
        // 가장 가까운 인스턴스
        Transform closet = null;
        float maxDistance = targetRange;
        foreach (var unit in units)
        {
            float distance = Vector3.Distance(transform.position, unit.transform.position);

            if (distance < maxDistance)
            {
                // 가장 가까운거 찾아서 순회탐색중입니다.
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
