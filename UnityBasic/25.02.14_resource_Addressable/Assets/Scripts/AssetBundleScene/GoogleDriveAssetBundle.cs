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
        // �ش� URI�� ���d �ؽ��� ������Ʈ
        UnityWebRequest wwr = UnityWebRequestTexture.GetTexture(uri);

        yield return wwr.SendWebRequest();

        if (wwr.result == UnityWebRequest.Result.Success)
        {
            var texture = ((DownloadHandlerTexture)wwr.downloadHandler).texture;
            Debug.Log("�ٿ�ε� ��");
            var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero, 1.0f);

            Debug.Log(texture.width  + "                 " + texture.height);
            image.sprite = sprite;
        }
        else
        {
            Debug.Log("������");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
