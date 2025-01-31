using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOUT : MonoBehaviour
{
    [SerializeField] GameObject gobj_background;
    private SpriteRenderer img_background;
    private Color color;
    [SerializeField] private float delay = 0.1f;

    IEnumerator FadeInOut()
    {
        float cnt = 0.0f;
        bool isFadeIn = true;
        while (true)
        {
            // ����ä�� ��������
            if (cnt >= 1.0f)
            {
                isFadeIn = false;
            }
            else if (cnt <= 0)
            {
                isFadeIn = true;
            }

            // ���� ����ä�� �����ƾ
            if (isFadeIn)
            {
                cnt += 0.1f;
            }
            else
            {
                cnt -= 0.1f;
            }

            //���İ� ���� ����
            color.a = cnt;
            img_background.color = color;
            yield return new WaitForSeconds(delay);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        img_background = gobj_background.GetComponent<SpriteRenderer>();
        color = img_background.color;
        color.a = 0;
        StartCoroutine(FadeInOut());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
