using System.Threading.Tasks;
using UnityEngine;

public class AsyncSample : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("작업 시작");
        Sample1();
        Debug.Log("Process A");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private async void Sample1()
    {
        Debug.Log("Process B");
        await Task.Delay(5000);

        Debug.Log("Process C");
    }
}
