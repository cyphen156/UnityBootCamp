using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public static int hp;
    public static string state;
    bool inDamage = false;

    /// <summary>
    /// "PlayerDown"
    /// , "PlayerUp"
    /// , "PlayerLeft"
    /// , "PlayerRight"
    /// , "PlayerDead"
    /// </summary>
    public List<string> animList;

    string currentAnim;
    string prevAnim;
    float h;    //  Horizontal
    float v;    //  Vertical

    Animator animator;
    Rigidbody2D rb;

    bool isMove;

    [SerializeField] public float z;   // rotation
    void Start()
    {
        speed = 3.0f;
        
        animList = new List<string>() {
            "PlayerDown"
            , "PlayerUp"
            , "PlayerLeft"
            , "PlayerRight"
            , "PlayerDead"
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

    void Update()
    {
        if (state != "Playing" || inDamage)
        {
            return;
        }

        
        if (!isMove)
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
        }

        Vector2 from = transform.position;

        Vector2 to = new Vector2(from.x + h, from.y + v);

        z = GetAngle(from, to);

        if (z >= -45 && z <= 45)
        {
            // right
            currentAnim = animList[3];
        }
        else if (z >= 45 && z <= 135)
        {
            // up
            currentAnim = animList[1];
        }
        else if (z >= -135 && z <= -45)
        {
            // down
            currentAnim = animList[0];
        }
        else 
        {
            // left
            currentAnim = animList[2];
        }
        if (currentAnim != prevAnim)
        {
            prevAnim = currentAnim;
            animator.Play(currentAnim);
        }
    }

    private void FixedUpdate()
    {
        if (state != "Playing" || inDamage)
        {
            return;
        }

        if (inDamage)
        {
            float value = Mathf.Sin(Time.time * 50);

            if (value > 0)
            {
                GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().enabled = false;
            }

            return;
        }


        rb.linearVelocity = new Vector2(h, v) * speed;
    }
    private float GetAngle(Vector2 from, Vector2 to)
    {
        float angle;

        if (h != 0 || v != 0)
        {
            //from과 to의 차이를 계산합니다.
            float dx = to.x - from.x;
            float dy = to.y - from.y;

            float radian = Mathf.Atan2(dy, dx);
            //Atan 같은 경우는 x좌표가 0일 경우 계산이 안됩니다.
            angle = radian * Mathf.Rad2Deg;
        }
        else
        {
            angle = z;
        }
        return angle;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetDamage(collision.gameObject);
    }
    void GetDamage(GameObject go)
    {
        if(state == "Playing")
        {
            hp--;
            if (hp > 0)
            {
                rb.linearVelocity = new Vector2(0, 0);

                Vector3 to = (transform.position - go.transform.position).normalized;

                rb.AddForce(new Vector2(to.x * 4, to.y * 4), ForceMode2D.Impulse);

                inDamage = true;

                Invoke("OnDamageExit", 0.25f);
            }

            else
            {
                GameOver();
            }
        }
    }

    void OnDamageExit()
    {
        inDamage = false;
        GetComponent<SpriteRenderer>().enabled = true;
    }
    private void GameOver()
    {
        state = "GameOver";
        GetComponent<CircleCollider2D>().enabled = false;
        rb.linearVelocity = new Vector2(0, 0);
        rb.gravityScale = 1;
        rb.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);

        GetComponent<Animator>().Play(animList[4]);

        Destroy(gameObject, 1f);
    }
}
