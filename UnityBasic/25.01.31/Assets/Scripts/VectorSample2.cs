using UnityEngine;

public class VectorSample2 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 a = new Vector3(1, 2, 0);
        // 정규화
        a.Normalize();
        
        // 거리 연산
        Vector3 b = new Vector3(0, 1, 0);
        float dist = Vector3.Distance(a, b);

        // 선형 보간
        Vector3 Res = Vector3.Lerp(a, b, 0.5f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
