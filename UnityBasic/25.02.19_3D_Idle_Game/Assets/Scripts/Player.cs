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
            Debug.Log("��ǥ�� ��ã�ٵ�");
        }

        playerPosition = transform.position;
        rotation = transform.rotation;
        distance = Vector3.Distance(transform.position, target.transform.position);

        
        // ���� ������ �Ÿ����� Ÿ�ٰ��� �Ÿ��� �۾��� ���
        if (evadeDistance > distance)
        {
            MoveEvade();
        }
    }

    private void FixedUpdate()
    {
        // ���ݹ��� �ȿ� ���Դ�. 
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

    // Ÿ���� ȸ���ϴ� �⵿
    public void MoveEvade()
    {
        Vector3 evadeDirection = (transform.position - target.position).normalized;
        transform.position += evadeDirection * evadeSpeed * Time.deltaTime;

        Debug.Log("ȸ�� �⵿ ����: " + evadeDirection);
    }
}
