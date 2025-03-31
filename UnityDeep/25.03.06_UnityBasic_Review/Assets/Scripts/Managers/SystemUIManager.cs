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
            Destroy(gameObject); // ���� �ν��Ͻ��� ������ ���� �������� ����
            return;
        }
    }
    
    private void Update()
    {
        
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // �ߺ� ����
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // �̺�Ʈ ����
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"�� �ε� �Ϸ�: {scene.name}");

        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayBGM(scene.name, 1.0f);
            Debug.Log($"BGM ���: {scene.name}");
        }
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Debug.Log("�� ���� :: " + sceneName);
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
