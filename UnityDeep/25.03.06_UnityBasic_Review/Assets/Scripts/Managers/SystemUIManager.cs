using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SystemUIManager : MonoBehaviour
{
    public static SystemUIManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // 기존 인스턴스가 있으면 새로 생성하지 않음
            return;
        }
    }
    
    private void Update()
    {
        
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // 중복 제거
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // 이벤트 해제
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"씬 로드 완료: {scene.name}");

        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayBGM(scene.name, 1.0f);
            Debug.Log($"BGM 재생: {scene.name}");
        }
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Debug.Log("씬 변경 :: " + sceneName);
    }
    
    public void StartButton()
    {
        StartCoroutine(ButtonClicked("tutorialScene"));
    }
    public void ExitButton()
    {
        StartCoroutine(ButtonClicked());
        Application.Quit();
    }
    IEnumerator ButtonClicked()
    {
        SoundManager.Instance.PlaySfx("ButtonClick", transform.position);
        yield return new WaitForSeconds(2.5f);
    }
    IEnumerator ButtonClicked(string sceneName)
    {
        SoundManager.Instance.PlaySfx("ButtonClick", transform.position);
        yield return new WaitForSeconds(2.5f);
        LoadScene(sceneName);
    }
}
