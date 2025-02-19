using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class Player : Character
{
    Vector3 playerPosition;
    Quaternion rotation;
    public float evadeDistance;
    public float evadeSpeed = 1f;
    float distance = 1000f;
    public Camera mainCamera;
    Vector3 cameraOffset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        mainCamera = Camera.main;
        playerPosition = new Vector3 (0, 0, 0);
        rotation = Quaternion.identity;
        attackRange = 70f;
        targetRange = 50f;
        evadeDistance = 30f;
        cameraOffset = mainCamera.transform.position - transform.position;

        //StartCoroutine(MoveEvade());
    }


    private void Update()
    {
        TargetSearch(Spawner.monsterList.ToArray());
        if (target == null)
        {
            Debug.Log("목표를 못찾겟따");
        }

        playerPosition = transform.position;
        rotation = transform.rotation;
        distance = Vector3.Distance(transform.position, target.transform.position);

        
        // 만약 설정된 거리보다 타겟과의 거리가 작아진 경우
        if (evadeDistance > distance)
        {
            MoveEvade();
        }
    }

    private void FixedUpdate()
    {
        // 공격범위 안에 들어왔다. 
        if (targetRange > distance)
        {
            Attack(target);
        }
    }

    private void LateUpdate()
    {
        Vector3 targetCameraPosition = transform.position + cameraOffset;
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetCameraPosition, 0.1f);
    }

    // 타겟을 회피하는 기동
    public void MoveEvade()
    {
        Vector3 evadeDirection = (transform.position - target.position).normalized;
        transform.position += evadeDirection * evadeSpeed * Time.deltaTime;

        Debug.Log("회피 기동 실행: " + evadeDirection);
    }
}
