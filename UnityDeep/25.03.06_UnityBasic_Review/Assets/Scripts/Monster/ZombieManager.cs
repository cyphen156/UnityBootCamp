using System;
using System.Collections;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    public enum  EZombieState
    {
        Patrol,
        Chase,
        Attack,
        Evade,
        Damage,
        Die,

        Idle
    }
    public EZombieState currentState = EZombieState.Idle;
    
    public float HP = 10f;
    public GameObject hitEffect;
    public float attackRange = 1.0f;
    public Transform target;
    public float attackDelay = 2.0f;
    private float nextAttackTime = 0.0f;
    public Transform[] patrolPoints;    // 순찰 포인트
    private float currentPatrolPoints;  // 현재 패트롤 인덱스
    public float moveSpeed = 2.0f;
    public float trackingRange = 5.0f;
    private bool isAttack = false;
    private float evadeRange = 5.0f;
    private float distanceToTarget;
    private bool isWaiting = true;
    public float idleTime = 2.0f;






    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
        distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (currentState == EZombieState.Patrol)
        // 기본적으로 순찰중일때 하는 행동
        {

        }

        if (distanceToTarget  < trackingRange)
        // 플레이어가 추적 범위 안에 들어옴
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
            currentState = EZombieState.Chase;
            
            if (distanceToTarget < attackRange && !isAttack)
            // 추적 범위 안에서도 공격 범위 안에 들어옴
            {
                isAttack = true;
                currentState = EZombieState.Attack;
                StartCoroutine(Attack());
                // 공격
            }
        }
        else
        // 추적 범위 안에 플레이어가 없음
        {

        }
        
        
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(nextAttackTime);
        isAttack = false;
    }

    public void OnHit()
    {
        Debug.Log("OnHit called");
        HP--;
        Instantiate(hitEffect, transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //other.GetComponent<MeshRenderer>().material.color = Color.red;
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어랑 충돌함");
            GameObject player = other.gameObject;
            if (player)
            {
                //playerManager.Weapon
            }

            // 처음으로 돌아가라
            player.GetComponent<PlayerManager>().ResetSequence();
        }
    }
}
