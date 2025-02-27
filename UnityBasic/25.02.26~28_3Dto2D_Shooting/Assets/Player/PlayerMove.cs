using System;
using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public Vector3 startPosition;
    public bool inputLock;

    private float maxX;
    private float maxY;
    float x;
    float y;
    float z;
    quaternion startRotate;
    public bool isEndOfMap;
    public TextMeshProUGUI playerText;
    public GameObject EnemyManager;
    private void Start()
    {
        maxX = 15;
        maxY = 15;
        inputLock = true;
        isEndOfMap = false;
        speed = 5.0f;
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;
        startRotate = transform.rotation;

        StartCoroutine(startSequence());
    }
    
    IEnumerator startSequence()
    {
        yield return new WaitForSeconds(0.2f);

        while (transform.position.y < 0)
        {
            if (transform.position.y < -5 || transform.position.z < 0 || transform.rotation.x > 0)
            // 혹시모르니까 일단 박아놔
            {
                //-5 to 0
                //- 3 to 0 && 90 to 0
                if (transform.rotation.x > 0)
                {
                    Quaternion targetRotation = Quaternion.Euler(0, 0, 0); // 목표 회전값
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 90f * Time.deltaTime / 8);
                }
                if (transform.position.z < 0)
                {
                    transform.position += new Vector3(0, 0, 1) * speed / 2 * Time.deltaTime / 8;
                }
            }
            transform.position += new Vector3(0, 1, 0) * speed / 2 * Time.deltaTime / 8;
            if (transform.position.y > -5)
            {
                // 배속재생
                transform.position += new Vector3(0, 1, 0) * speed / 2 * Time.deltaTime;
            }
            yield return null;
        }
        inputLock = false;

        playerText.text = ("살아남으세요!");
        EnemyManager.SetActive(true);
        playerText.text = ("");
    }

    private void Update()
    {
        if (!inputLock)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Vector3 dir = new Vector3(h, v, 0);

            transform.rotation = quaternion.Euler(0, -h, 0);
            
            transform.position += dir * speed * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameObject.GetComponent<Fire>().FireBullet();
            }

            // 맵 영역을 나가지 못하도록 하는 시퀀스
            x = 0;
            y = 0;

            if (Mathf.Abs(transform.position.x) >= maxX)
            {
                isEndOfMap = true;
                x = -2 * transform.position.x;
            }
            if (Mathf.Abs(transform.position.y) >= maxY)
            {
                isEndOfMap = true;
                y = -2 * transform.position.y;
            }
            transform.position += new Vector3(x, y, 0);
            if (isEndOfMap)
            {
                Camera.main.transform.position += new Vector3(x, y, 0);
                isEndOfMap = false;
            }
        }
    }

    public float GetSpeed()
    {
        return speed;
    }
}
