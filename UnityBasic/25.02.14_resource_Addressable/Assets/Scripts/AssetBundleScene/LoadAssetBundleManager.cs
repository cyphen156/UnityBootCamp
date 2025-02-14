using System.IO;
using System.Collections;
using UnityEngine;

public class LoadAssetBundleManager : MonoBehaviour
{
    string path = "Assets/Bundle/asset1";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(LoadAsync(path));
    }

    IEnumerator LoadAsync(string path)
    {
        AssetBundleCreateRequest request = AssetBundle.LoadFromMemoryAsync(File.ReadAllBytes(path));

        yield return request;

        AssetBundle bundle = request.assetBundle;

        var prefab = bundle.LoadAsset<Material>("red");
        if (prefab == null)
        {
        Debug.Log(prefab);

        }
        //var prefab2 = bundle.LoadAsset<Material>("blue");
        //if (prefab2 == null)
        //{
        //    Debug.Log(prefab2);

        //}
        //var prefab3 = bundle.LoadAsset<Material>("green");
        //if (prefab3 == null)
        //{
        //    Debug.Log(prefab3);

        //}
        Instantiate(prefab);

        //GameObject prefab2 = bundle.LoadAsset<GameObject>("asset2");
        //Instantiate(prefab);
        //GameObject prefab33 = bundle.LoadAsset<GameObject>("asset3"); 
        //Instantiate(prefab);

    }
}