using UnityEngine;

public class Sample : MonoBehaviour
{
    float deltaTimeAccumulator; // 누적된 델타 타임
    float interval = 1f;        // 1초 간격
    int cnt;                    // 카운트 변수

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        deltaTimeAccumulator = 0f;
        cnt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // 프레임 간 경과 시간 누적
        deltaTimeAccumulator += Time.deltaTime;

        // 누적된 시간이 설정된 간격(1초)을 초과했을 때
        if (deltaTimeAccumulator >= interval)
        {
            deltaTimeAccumulator -= interval; // 간격만큼 빼기
            Debug.Log(cnt++);                 // 카운트 출력
        }
    }
}
