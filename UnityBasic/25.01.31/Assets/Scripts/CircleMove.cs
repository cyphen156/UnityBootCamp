using UnityEngine;

public class CircleMove : MonoBehaviour
{
    // circle�� ������ ��ġ�� Lerp ��Ű�� ��ũ��Ʈ
    public GameObject circle;
    Vector3 position = new Vector3(8, -3, 0);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        circle.transform.position = Vector3.Lerp(circle.transform.position, position, Time.deltaTime);
        // 0 To 1
        // ������ �ӵ��� ��ǥ���� �̵��ϰ� ����� ��ũ��Ʈ

        circle.transform.position = Vector3.MoveTowards(circle.transform.position, position, Time.deltaTime);

        // ȸ�� ����
        circle.transform.position = Vector3.Slerp(circle.transform.position, position, 0.05f);
    }
}
