using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextCnt : MonoBehaviour
{
    // Count를 초마다 증가하면서 UI상에 뿌려주는 역할의 스크립트
    public GameObject gobj_text;

    public Text text;
    private int cnt = 0;
    IEnumerator CntPlus() // interface Enum Iterator
    {
        while (true)
        {
            // CPU의 권한을 다른 함수에 위임   --> 비동기함수
            yield return new WaitForSeconds(1);
            Debug.Log("밥만 먹고 올게");
            //yield return new WaitForSeconds(5);
            Debug.Log("밥 다 먹고 왔어");
            cnt++;
            // N0 == tntwkfmf 3자리 간격으로 ,를 표시하는 format 1000 => 1,000
            text.text = cnt.ToString("N0");

            if (cnt > 100)
            {
                Debug.Log("나 이제 일 안할거야");
                break;
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gobj_text = GameObject.FindGameObjectWithTag("Text001");
        // 함수가 아닌 문자열을 찾아서 실행시키기에 중지시킬 수 있음
        // but 성능상의 이슈
        StartCoroutine("CntPlus");
        
        StopCoroutine("CntPlus");
        // 함수를 실행시키기 때문에 중지시키지 못함
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
