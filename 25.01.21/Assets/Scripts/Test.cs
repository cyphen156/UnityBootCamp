using UnityEngine;

public class SampleA
{
    
}


/// <summary>
/// 처음으로 만들어본 유니티의 스크립트
/// </summary>
/// 
public class Test : MonoBehaviour
{
    /// <summary> dsdsds
    
    int cnt = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("오늘은 C# 스크립트를 배우는 날입니다.");   
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"count = {cnt}" );
    }
}
