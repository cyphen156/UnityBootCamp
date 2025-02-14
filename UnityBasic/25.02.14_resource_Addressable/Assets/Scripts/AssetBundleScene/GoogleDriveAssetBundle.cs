using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static System.Net.WebRequestMethods;

public class GoogleDriveAssetBundle : MonoBehaviour
{
    private string uri = "https://docs.google.com/uc?export=download&id=1NTWnvNU7-5TLv_wXT70Z4gE1nWy5b8O2";

    public Image image;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine("DownloadIMG");
    }

    IEnumerator DownloadIMG()
    {
        // 해당 URI를 통햏 텍스쳐 리퀘스트
        UnityWebRequest wwr = UnityWebRequestTexture.GetTexture(uri);

        yield return wwr.SendWebRequest();

        if (wwr.result == UnityWebRequest.Result.Success)
        {
            var texture = ((DownloadHandlerTexture)wwr.downloadHandler).texture;
            Debug.Log("다운로드 함");
            var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero, 1.0f);

            Debug.Log(texture.width  + "                 " + texture.height);
            image.sprite = sprite;
        }
        else
        {
            Debug.Log("실패함");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
