using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// this component controlls MainCamera 
/// always together whit Player Object
/// 
/// </summary>
public class CameraController : MonoBehaviour
{
    public float maxDistX;
    public float maxDistY;
    public Vector3 dist;

    public GameObject player;
    public float cameraSpeed;

    void Start()
    {
        maxDistX = 2.0f;
        maxDistY = 3.0f;
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(GetCameraSpeed());
        StartCoroutine(startSequence());

    }
    IEnumerator GetCameraSpeed()
    {
        yield return new WaitForSeconds(0.2f);
        cameraSpeed = player.GetComponent<PlayerMove>().GetSpeed();
        //yield return new WaitForSeconds(1);
    }
    IEnumerator startSequence()
    {
        yield return new WaitForSeconds(0.2f);

        // ��ǥ�� 0, 0, -5, 0
        while (transform.position.y < 0 || transform.position.z > -5)
        {
            // ȸ������ �׻� �����ؾ���
            Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 180f * Time.deltaTime / 8);
            // z���̵� = Player.2��
            transform.position += new Vector3(0, 0, -2) * cameraSpeed * Time.deltaTime / 8;
            
            // Y�� �̵��� ������ �Դٸ� ���ٸ� �ؾ� �� �� ����
            if (transform.position.z < 0)
            {
                transform.position += new Vector3(0, 1, 0) * cameraSpeed * Time.deltaTime * 1.7f / 8;
            }
            else 
            {
                transform.position += new Vector3(0, -1, 0) * cameraSpeed * Time.deltaTime * 0.5f / 8;
            }

            float rotationX = transform.rotation.eulerAngles.x;
            if (rotationX > 180) rotationX -= 360;

            Debug.Log(rotationX);
            if (rotationX > -30)
            {
                transform.position += new Vector3(0, 1, 0) * cameraSpeed * Time.deltaTime * 1.7f / 8;
            }
            yield return null;
        }
    }

    private void LateUpdate()
    {
        if (player == null)
        {
            gameObject.SetActive(false);
        }
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, v, 0);
        
        dist = transform.position - player.transform.position;
        
        if (Mathf.Abs(dist.x) > maxDistX)
        {
            Camera.main.transform.position += dir * cameraSpeed * Time.deltaTime;
        }
        else if (Mathf.Abs(dist.y) > maxDistY)
        {
            Camera.main.transform.position += dir * cameraSpeed * Time.deltaTime;
        }
    }
}
