using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextCnt : MonoBehaviour
{
    // Count�� �ʸ��� �����ϸ鼭 UI�� �ѷ��ִ� ������ ��ũ��Ʈ
    public GameObject gobj_text;

    public Text text;
    private int cnt = 0;
    IEnumerator CntPlus() // interface Enum Iterator
    {
        while (true)
        {
            // CPU�� ������ �ٸ� �Լ��� ����   --> �񵿱��Լ�
            yield return new WaitForSeconds(1);
            Debug.Log("�丸 �԰� �ð�");
            //yield return new WaitForSeconds(5);
            Debug.Log("�� �� �԰� �Ծ�");
            cnt++;
            // N0 == tntwkfmf 3�ڸ� �������� ,�� ǥ���ϴ� format 1000 => 1,000
            text.text = cnt.ToString("N0");

            if (cnt > 100)
            {
                Debug.Log("�� ���� �� ���Ұž�");
                break;
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gobj_text = GameObject.FindGameObjectWithTag("Text001");
        // �Լ��� �ƴ� ���ڿ��� ã�Ƽ� �����Ű�⿡ ������ų �� ����
        // but ���ɻ��� �̽�
        StartCoroutine("CntPlus");
        
        StopCoroutine("CntPlus");
        // �Լ��� �����Ű�� ������ ������Ű�� ����
        StartCoroutine(CntPlus());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //gobj_text
    }
}
