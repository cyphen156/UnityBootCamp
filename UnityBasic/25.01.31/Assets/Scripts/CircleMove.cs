using UnityEngine;

public class CircleMove : MonoBehaviour
{
    // circle을 지정된 위치로 Lerp 시키는 스크립트
    public GameObject circle;
    Vector3 position = new Vector3(8, -3, 0);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        circle.transform.position = Vector3.Lerp(circle.transform.position, position, Time.deltaTime);
        // 0 To 1
        // 일정한 속도로 목표까지 이동하게 만드는 스크립트

        circle.transform.position = Vector3.MoveTowards(circle.transform.position, position, Time.deltaTime);

        // 회전 보간
        circle.transform.position = Vector3.Slerp(circle.transform.position, position, 0.05f);
    }
}
