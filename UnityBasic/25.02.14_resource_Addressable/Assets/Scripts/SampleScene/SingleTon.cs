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
        // Ȥ�ø� �ʱ�ȭ ���� ����
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
            // Ȥ�ø� �ʱ�ȭ ���� ����
            if (instance == null)
            {
                instance = new SingleTon();
            }
            return instance;
        }
    }
}
