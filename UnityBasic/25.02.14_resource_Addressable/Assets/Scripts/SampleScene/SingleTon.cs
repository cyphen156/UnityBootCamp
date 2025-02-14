using UnityEngine;

public class SingleTon : MonoBehaviour
{
    private static SingleTon instance;

    public int point = 0;

    public void PointPlus()
    {
        point++;
    }
    public static SingleTon GetInst()
    {
        // 혹시모를 초기화 실패 방지
        if (instance == null)
        {
            instance = new SingleTon();
        }
        return instance;
    }
    public void ViewPoint()
    {
        Debug.Log(point);
    }
    public static SingleTon Instance
    {
        get
        {
            // 혹시모를 초기화 실패 방지
            if (instance == null)
            {
                instance = new SingleTon();
            }
            return instance;
        }
    }
}
