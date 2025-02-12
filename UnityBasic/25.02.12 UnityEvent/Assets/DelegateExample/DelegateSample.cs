using UnityEditor.PackageManager;
using UnityEngine;

public class DelegateSample : MonoBehaviour
{

    public delegate void SampleDelegate();
    // �͸��Լ� ��������
    // ������Ҷ� ���ǰ�������.

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
