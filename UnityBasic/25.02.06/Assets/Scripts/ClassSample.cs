using UnityEngine;

class Unit
{
    // Ŭ������ ���� ��ü�� ������ �ۼ�
    // Field
    public string uint_name;

    // method
    public static void func1()
    {

    }

    public void func2() { }
}

public class ClassSample : MonoBehaviour
{
    // Ŭ���� �ν��Ͻ�
    Unit unit;

    void Start()
    {
        unit.uint_name = "";

        Unit.func1();

        unit.func2();
    }

    private void Update()
    {
        
    }
}
