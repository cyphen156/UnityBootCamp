using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public int hp = 3;
    public float speed = 0.5f;
    public float patternDistance = 4.0f;
    public List<string> animList;

    string currentAnim;
    string prevAnim;
    float h;    //  Horizontal
    float v;    //  Vertical

    Animator animator;
    Rigidbody2D rb;

    bool isMove;

    [SerializeField] public float z;   // rotation

    public int arrangedId = 0;
    void Start()
    {
        speed = 0.5f;
        animList = new List<string>() {
            "MonsterIdle"
            ,"MonsterDown"
            , "MonsterUp"
            , "MonsterLeft"
            , "MonsterRight"
            , "MonsterDead"
        };

        prevAnim = animList[0];
        currentAnim = prevAnim;
        h = 0f;
        v = 0f;

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        isMove = false;

        z = -90f;

        hp = 3;
    }

    // Update is called once per frame
    void Update()
    {
        var player = GameObject.FindGameObjectWithTag("Plaer");
     
        if (player != null)
        {
            if (isMove)
            {
                float dx = player.transform.position.x - transform.position.x;
                float dy = player.transform.position.y - transform.position.y;

                float rad = Mathf.Atan2(dy, dx);
                float degree = rad * Mathf.Rad2Deg;

                if (degree >= -45.0f && degree <= 45.0f)
                {
                    currentAnim = animList[4];
                }
                else if (degree >= 45.0f && degree <= 135.0f)
                {
                    currentAnim = animList[2];
                }
                else if (degree >= 135.0f && degree <= -45.0f)
                {
                    currentAnim = animList[1];
                }
                else
                {
                    currentAnim = animList[3];
                }

                h = Mathf.Cos(rad) * speed;
                v = Mathf.Sin(rad) * speed;
            }
            else
            {
                float distance = Vector2.Distance(transform.position, player.transform.position);

                if (distance <= patternDistance)
                {
                    isMove = true;
                }
            }
        }
        else if(isMove)
        {
            isMove = false;
            rb.linearVelocity = new Vector2(0, 0);
        }
    }

    private void FixedUpdate()
    {
        if(isMove && hp > 0)
        {
            rb.linearVelocity = new Vector2(h, v);

            if (currentAnim != prevAnim)
            {
                prevAnim = currentAnim;
                var animator = GetComponent<Animator>();
                animator.Play(currentAnim);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            hp--;

            if (hp <= 0)
            {
                GetComponent<CircleCollider2D>().enabled = false;


                rb.linearVelocity = new Vector2(0, 0);

                var anim = GetComponent<Animator>();
                anim.Play(animList[5]);

                Destroy(gameObject, 0.5f);
            }
        }
    }
}
