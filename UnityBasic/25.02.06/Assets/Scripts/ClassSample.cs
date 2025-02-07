using UnityEngine;

class Unit
{
    // 클래스로 만들 객체의 정보를 작성
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
    // 클래스 인스턴스
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
