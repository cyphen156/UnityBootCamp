using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SampleSceneManager : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"현재 로드된 씬의 이름은 {scene.name}임다.");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            // default :: LoadSceneMode.Single
            // 씬이 대체된다 :: 데이터 날아간다
            SceneManager.LoadScene("BRP Sample Scene");
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            // 씬이 하나 더 로드된다 :: 덮어씌운다 :::: 화면 플리커링 될수도 있으니 주의한다.
            SceneManager.LoadScene("BRP Sample Scene", LoadSceneMode.Additive);
        }
        //StartCoroutine(AsyncLoad());
    }
    //IEnumerator AsyncLoad()
    //{
    //    if (Input.GetKeyDown(KeyCode.U))
    //    {
    //        // 씬이 하나 더 로드된다 :: 덮어씌운다 :::: 화면 플리커링 될수도 있으니 주의한다.
    //        // 비동기 로드
    //        // --> 로드가 끝날때까지 '나'는 기다려
    //        SceneManager.LoadSceneAsync("BRP Sample Scene", LoadSceneMode.Additive);
    //    }
    //}
}
