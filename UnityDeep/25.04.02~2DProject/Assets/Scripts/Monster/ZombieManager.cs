using System.Collections;
using UnityEngine;


public enum MonsterType
{
    None, FlyingMonster
}

public enum StateType
{
    Idle, PatrolWalk, PatrolRun, ChaseWalk, ChaseRun, StrongAttack, Attack
}
public class ZombieManager : MonoBehaviour
{
    private Color originalColor;
    private Renderer objectRenderer;
    public float colorChangeDuration = 0.5f;
    public float monsterHp = 10.0f;
    public float speed = 2.0f;
    public float maxDistance = 3.0f;
    private Vector3 startPos;
    private int direction = 1;
    public GroundType currentGroundType;
    public MonsterType monsterType = MonsterType.None;
    public float damage = 1.0f;
    public StateType stateType = StateType.Idle;

    public Transform player;
    public float chaseRange = 5.0f;
    public float attackRange = 1.5f;
    public bool isAttacking = false;

    private float stateChanageInterval = 3.0f;
    private Coroutine stateChangeRoutine;

    void Start()
    {
        objectRenderer = GetComponent<SpriteRenderer>();
        originalColor = objectRenderer.material.color;
        startPos = transform.position;
        int randomChoice = Random.Range(0, 1);
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        stateChangeRoutine = StartCoroutine(RandomStateChanger());
    }

    private void Update()
    {
        if (monsterType == MonsterType.None || player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange && !isAttacking)
        {
            if (stateType != StateType.Attack)
            {
                StopAllCoroutines();
                stateType = StateType.StrongAttack;
                StartCoroutine(AttackRoutine());
            }
            return;
        }
        if (distanceToPlayer <= chaseRange)
        {
            if (stateType != StateType.ChaseWalk && stateType != StateType.ChaseRun)
            {
                if (stateChangeRoutine != null)
                {
                    StopCoroutine(stateChangeRoutine);
                }

                int chaseType = Random.Range(0, 2);
                stateType = chaseType == 0 ? StateType.ChaseWalk : StateType.ChaseRun;
                Debug.Log($"[상태 전환] 추적 상태 : {stateType}");
            }

            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            float chaseSpeed = stateType == StateType.ChaseRun ? speed * 2 : speed;
            transform.position += directionToPlayer * chaseSpeed * Time.deltaTime;
            return;
        }

        if ((stateType == StateType.ChaseWalk || stateType == StateType.ChaseRun) && distanceToPlayer > chaseRange)
        {
            Debug.Log("[상태 복귀] 추적 종료");
            stateType = StateType.Idle;
            if (stateChangeRoutine == null)
            {
                stateChangeRoutine = StartCoroutine(RandomStateChanger());
            }
        }

        if (stateType == StateType.Attack) return;

        PartrolMovement();
    }

    private void PartrolMovement()
    {
        if (currentGroundType == GroundType.UpGround)
        {
            if (stateType == StateType.PatrolWalk || stateType == StateType.PatrolRun)
            {
                if (transform.position.y > startPos.y + maxDistance)
                {
                    direction = -1;
                }
                else if (transform.position.y < startPos.y - maxDistance)
                {
                    direction = 1;
                }
                float movespeed = stateType == StateType.PatrolRun ? speed * 2 : speed;
                transform.position += new Vector3(0, movespeed * direction * Time.deltaTime, 0);
            }

        }
        else
        {
            if (transform.position.x > startPos.x + maxDistance)
            {
                direction = -1;
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (transform.position.x < startPos.x - maxDistance)
            {
                direction = 1;
                GetComponent<SpriteRenderer>().flipX = false;
            }

            transform.position += new Vector3(speed * direction * Time.deltaTime, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            StartCoroutine(ChangeColorTemporatily());
        }
    }

    IEnumerator ChangeColorTemporatily()
    {
        SoundManager.Instance.PlaySFX(SFXType.Hit);
        objectRenderer.material.color = Color.red;
        yield return new WaitForSeconds(colorChangeDuration);
        objectRenderer.material.color = originalColor;
    }

    IEnumerator RandomStateChanger()
    {
        while (true)
        {
            yield return new WaitForSeconds(stateChanageInterval);
            int randomState = Random.Range(0, 3);
            stateType = (StateType)randomState;
            Debug.Log($"[랜덤 상태 전환] 현재 상태 : {stateType}");
        }
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;
        Debug.Log("[공격 상태] 공격 시작");
        yield return new WaitForSeconds(1.0f);
        isAttacking = false;
        Debug.Log("[공격 상태] 공격 종료, 상태 복귀");
        stateChangeRoutine = StartCoroutine(RandomStateChanger());
    }
}