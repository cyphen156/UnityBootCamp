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
            // 알파채널 증감설정
            if (cnt >= 1.0f)
            {
                isFadeIn = false;
            }
            else if (cnt <= 0)
            {
                isFadeIn = true;
            }

            // 실제 알파채널 변경루틴
            if (isFadeIn)
            {
                cnt += 0.1f;
            }
            else
            {
                cnt -= 0.1f;
            }

            //알파값 적용 렌더
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
