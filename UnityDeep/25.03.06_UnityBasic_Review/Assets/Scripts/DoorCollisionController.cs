using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DoorCollisionController : MonoBehaviour
{
    public float rotationTime = 3.0f;

    public float targetAngle;
    public float torqueForce = 20f;           // 충돌 시 회전 세기
    public float autoRotateForce = 30f;       // 상호작용 시 회전 세기
    public float maxAngle = -90f;          // 최대 회전 각도
    public float angleTolerance = 1f;         // 오차 허용

    private Rigidbody rb;
    private bool isOpen = false;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 30.0f; 
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("콜리전");
        // 상대 속도 방향으로 회전력 계산
        Vector3 forceDir = collision.relativeVelocity.normalized;
        Vector3 hingeAxis = transform.forward; // Z축 기준 회전

        Vector3 torque = Vector3.Cross(forceDir, hingeAxis) * torqueForce;
        Debug.Log(forceDir + "\t\t\t\t" + hingeAxis + "\t\t\t\t" + torque);
        rb.AddTorque(torque, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {

    }
    //public void ForceDoorRotate()
    //{
    //    StartCoroutine(ForceRotateDoor());
    //}

    //IEnumerator ForceRotateDoor()
    //{
    //    float elapsed = 0.0f;

    //    if (isOpen)
    //    {
    //        targetAngle = 0.0f;
    //    }
    //    else
    //    {
    //        targetAngle = maxAngle;
    //    }
    //    while (elapsed < rotationTime)
    //    {
    //        float offsetX = UnityEngine.Random.Range(-1.0f, 1.0f) * shakeMagnitude;
    //        float offsetY = UnityEngine.Random.Range(-1.0f, 1.0f) * shakeMagnitude;

    //        Camera.main.transform.position = originTransform.position + new Vector3(offsetX, offsetY, 0.0f);

    //        elapsed += Time.deltaTime;
    //        yield return null;
    //    }

    //    isOpen = !isOpen;
    //}

    //private void FixedUpdate()
    //{
    //    float targetY = isOpen ? 0f : targetAngle;
    //    float angleDiff = targetY - currentY;

    //    if (Mathf.Abs(angleDiff) <= angleTolerance)
    //    {
    //        isAutoRotating = false;
    //        isOpen = !isOpen;
    //        rb.angularVelocity = Vector3.zero;
    //        return;
    //    }

    //    float direction = Mathf.Sign(angleDiff);
    //    Vector3 torque = transform.up * direction * autoRotateForce;
    //    rb.AddTorque(torque, ForceMode.Force);
    //}


}
