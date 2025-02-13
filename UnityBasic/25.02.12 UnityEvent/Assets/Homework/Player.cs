using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : AbsGameObject
{
    public UIController gObjUIController;

    public float moveSpeed = 5f;
    public float rotateSpeed = 800f;
    [SerializeField] private float rx;
    [SerializeField] private float ry;

    private void Start()
    {
        hp = 100;
        rx = 0;
        ry = 0;
    }

    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        gObjUIController.SetUIText(hp.ToString(), coin.ToString());
    }

    private void Update()
    {
        // 캐릭터 회전
        float rotationX = Input.GetAxis("Mouse X");
        //float rotationY = Input.GetAxis("Mouse Y");
        rx += rotationX * rotateSpeed * Time.deltaTime;
        //ry += rotationY * rotateSpeed * Time.deltaTime;
        // 상/하단 각도제한
        //ry = Mathf.Clamp(ry, -45, 70);
        transform.eulerAngles = new Vector3 (0, rx, 0);
        // 캐릭터 이동
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 moveDir = transform.right * x + transform.forward * z;
        transform.position += moveDir.normalized * moveSpeed * Time.deltaTime;
    }
}
