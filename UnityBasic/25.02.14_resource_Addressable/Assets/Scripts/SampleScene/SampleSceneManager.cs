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
        Debug.Log($"���� �ε�� ���� �̸��� {scene.name}�Ӵ�.");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            // default :: LoadSceneMode.Single
            // ���� ��ü�ȴ� :: ������ ���ư���
            SceneManager.LoadScene("BRP Sample Scene");
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            // ���� �ϳ� �� �ε�ȴ� :: ������ :::: ȭ�� �ø�Ŀ�� �ɼ��� ������ �����Ѵ�.
            SceneManager.LoadScene("BRP Sample Scene", LoadSceneMode.Additive);
        }
        //StartCoroutine(AsyncLoad());
    }
    //IEnumerator AsyncLoad()
    //{
    //    if (Input.GetKeyDown(KeyCode.U))
    //    {
    //        // ���� �ϳ� �� �ε�ȴ� :: ������ :::: ȭ�� �ø�Ŀ�� �ɼ��� ������ �����Ѵ�.
    //        // �񵿱� �ε�
    //        // --> �ε尡 ���������� '��'�� ��ٷ�
    //        SceneManager.LoadSceneAsync("BRP Sample Scene", LoadSceneMode.Additive);
    //    }
    //}
}
