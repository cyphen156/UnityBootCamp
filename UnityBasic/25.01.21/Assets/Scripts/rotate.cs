using UnityEngine;

/// <summary>
/// 큐브를 회전시키는 컴포넌트
/// </summary>
public class rotate : MonoBehaviour
{
    #region value
    float deltaTimeAccumulator; // 누적된 델타 타임
    float interval = 1f;        // 1초 간격

    const float maxVal = 360.0f;
    const float minVal = 0.0f;
    float x;
    float y;
    float z;
    #endregion

    #region function
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.fixedDeltaTime = 1.0f / 60.0f;

        deltaTimeAccumulator = 0f;
        x = transform.rotation.x;
        y = transform.rotation.y;
        z = transform.rotation.z;
    }

    // Update is called once per frame
    void Update()
    {
        //// 프레임 간 경과 시간 누적
        //deltaTimeAccumulator += Time.deltaTime;
        ////float rotationAmount = maxVal / 60.0f; // 1초 동안 maxVal만큼 회전

        //// 누적된 시간이 설정된 간격(1초)을 초과했을 때
        ////if (deltaTimeAccumulator >= interval)
        //{
        //    deltaTimeAccumulator -= interval; // 간격만큼 빼기
        //    transform.Rotate(x-1 , y-1, z -1); 

        //   //transform.Rotate(x + 1 * rotationAmount, y + 1*rotationAmount, z + 0*rotationAmount);

        //}
    }


    void FixedUpdate()
    {
        // 각 FixedUpdate 호출마다 회전할 각도 계산
        float rotationAmount = maxVal / 60.0f; // 1초 동안 maxVal만큼 회전

        // 지정된 축으로 회전 적용
        transform.Rotate(x + 10 * rotationAmount, y + 1 * rotationAmount, z + 0 * rotationAmount);
    }
    #endregion
}
