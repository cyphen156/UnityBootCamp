using UnityEditor.PackageManager;
using UnityEngine;

public class DelegateSample : MonoBehaviour
{

    public delegate void SampleDelegate();
    // 익명함수 쓰지마라
    // 디버깅할때 개피곤해진다.

    private void Start()
    {
        SampleDelegate dt = new SampleDelegate(Attack);
        dt += Attack;
        dt += Guard;
        dt += MoveLeft;
        dt();
    }

    void Attack()
    {
        Debug.Log("Attack");
    }
    void Guard()
    {
        Debug.Log("Guard");

    }
    void MoveLeft()
    {
        Debug.Log("MoveLeft");

    }
}
