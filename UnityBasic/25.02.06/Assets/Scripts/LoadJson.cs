using System.IO;
using UnityEngine;

[System.Serializable]
public class Item01
{
    public int i;
    public float f;
    public double d;
    public string s;
    //public Vector3 vec3;
    //public int[] array;
    public bool b;
}

public class LoadJson : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string load_json = File.ReadAllText(Application.dataPath + "/newJson.json");

        //-> C# 스크립트에서는 ReadAllText메서드를 통해 파일을 읽어올 경우 주석문까지 모두 읽어옴 
        //-- > 추가 예외 처리를 하지 않는다면 클래스 인스턴스로 생성할 수 없다
        //--->엄격하게 포매팅 해야 사용가능
        Debug.Log(load_json);
        var json = JsonUtility.FromJson<Item01>(load_json);


        json.b = false;
        //json.vec3 = new Vector3(999, 999, 999);
        json.d = 9990.99f;
        File.WriteAllText(Application.dataPath + "/item002.json", JsonUtility.ToJson(json));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
