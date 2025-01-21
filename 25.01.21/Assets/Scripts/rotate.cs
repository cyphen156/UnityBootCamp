using UnityEngine;

/// <summary>
/// ť�긦 ȸ����Ű�� ������Ʈ
/// </summary>
public class rotate : MonoBehaviour
{
    #region value
    float deltaTimeAccumulator; // ������ ��Ÿ Ÿ��
    float interval = 1f;        // 1�� ����

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
        //// ������ �� ��� �ð� ����
        //deltaTimeAccumulator += Time.deltaTime;
        ////float rotationAmount = maxVal / 60.0f; // 1�� ���� maxVal��ŭ ȸ��

        //// ������ �ð��� ������ ����(1��)�� �ʰ����� ��
        ////if (deltaTimeAccumulator >= interval)
        //{
        //    deltaTimeAccumulator -= interval; // ���ݸ�ŭ ����
        //    transform.Rotate(x-1 , y-1, z -1); 

        //   //transform.Rotate(x + 1 * rotationAmount, y + 1*rotationAmount, z + 0*rotationAmount);

        //}
    }


    void FixedUpdate()
    {
        // �� FixedUpdate ȣ�⸶�� ȸ���� ���� ���
        float rotationAmount = maxVal / 60.0f; // 1�� ���� maxVal��ŭ ȸ��

        // ������ ������ ȸ�� ����
        transform.Rotate(x + 10 * rotationAmount, y + 1 * rotationAmount, z + 0 * rotationAmount);
    }
    #endregion
}
