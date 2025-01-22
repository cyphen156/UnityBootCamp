using System.Collections;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

/// <summary>
///  �÷��̾��� �̵� ����
/// </summary>

// �ش� ����� ���� �� ��ũ��Ʈ�� ������Ʈ�ν� ����� ���
// ������� ������Ʈ�� �ʼ������� �䱸��
// if component already added == deny delete component
// if component added not yet == automatically add nessasary component

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    private float x, y;
    public float speed;
    public float jumpVal;
    private Rigidbody2D rb;
    private bool isJump;
    //private bool isWall;
    private uint score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        x = 0f; // �ʱⰪ ����
        y= 0f;
        speed = 3f;
        jumpVal = 8f;
        rb = GetComponent<Rigidbody2D>();
        //isWall = false;
        isJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump();
        }
    }

    void FixedUpdate()
    {
        
    }

    void Move()
    {
        x = Input.GetAxisRaw("Horizontal");

        Vector3 velocity = new Vector3(x * speed * Time.deltaTime, y, 0);

        transform.position += velocity;
    }

    void jump()
    {
        if (!isJump)
        {
            isJump = true;
            rb.AddForce(new Vector3(0, jumpVal, 0), ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            score++;
            Debug.Log($"{score}");
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Goal"))
        {
            if (score <= 35)
            {
                Debug.Log("������ ���ڶ�� ������ �� ��ƿ�����.");
            }
            else if (score < 40)
            {
                Debug.Log("���߾��. �׷��� ��¦ �ƽ��׿� (TT)");
            }
            else
            {
                Debug.Log("�Ϻ��ؿ�! ���� ������.\n***********************\n");
                Destroy(collision.gameObject);
                
                Camera mainCamera = Camera.main;
                if (mainCamera != null)
                {
                    ActivateSpecificLayer(mainCamera, "end"); // "end" ���̾ Ȱ��ȭ
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isJump = false;
        }
    }

    void ActivateSpecificLayer(Camera camera, string layerName)
    {
        // Ư�� ���̾� �̸����� Ȱ��ȭ
        int layer = LayerMask.NameToLayer(layerName);
        if (layer >= 0)
        {
            camera.cullingMask = 1 << layer; // �ش� ���̾ Ȱ��ȭ
            Debug.Log($"Layer '{layerName}' Ȱ��ȭ �Ϸ�.");
        }
        else
        {
            Debug.LogError($"Layer '{layerName}'�� �������� �ʽ��ϴ�.");
        }
    }
}
