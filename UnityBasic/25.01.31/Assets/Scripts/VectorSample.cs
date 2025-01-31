using UnityEditor.SceneManagement;
using UnityEngine;

public class VectorSample : MonoBehaviour
{
    Vector3 vec = new Vector3();

    float x, y, z;
    
    Vector3 a = new Vector3(1, 2, 0);
    Vector3 b = new Vector3(3, 4, 0);
    // 초기화 해주세요
    //Vector3 custom_vec = new Vector3(x, y, z);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 c = a + b;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
