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
    public Transform[] patrolPoints;    // ���� ����Ʈ
    private float currentPatrolPoints;  // ���� ��Ʈ�� �ε���
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
        // �⺻������ �������϶� �ϴ� �ൿ
        {

        }

        if (distanceToTarget  < trackingRange)
        // �÷��̾ ���� ���� �ȿ� ����
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
            currentState = EZombieState.Chase;
            
            if (distanceToTarget < attackRange && !isAttack)
            // ���� ���� �ȿ����� ���� ���� �ȿ� ����
            {
                isAttack = true;
                currentState = EZombieState.Attack;
                StartCoroutine(Attack());
                // ����
            }
        }
        else
        // ���� ���� �ȿ� �÷��̾ ����
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
            Debug.Log("�÷��̾�� �浹��");
            GameObject player = other.gameObject;
            if (player)
            {
                //playerManager.Weapon
            }

            // ó������ ���ư���
            player.GetComponent<PlayerManager>().ResetSequence();
        }
    }
}
