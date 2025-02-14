using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddressableManager : MonoBehaviour
{
    // 
    public AssetReferenceGameObject MyAddressableCapsule;

    public GameObject gO = new GameObject();

    private void Start()
    {
        StartCoroutine(AddressableInit());
    }

    IEnumerator AddressableInit()
    {
        var init = Addressables.InitializeAsync(); 
        yield return init;
    }

    public void OnCreateButtonEnter()
    {
        MyAddressableCapsule.InstantiateAsync().Completed += (obj) =>
        {
            gO = obj.Result;
        };
    }

}
