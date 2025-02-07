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

        //-> C# ��ũ��Ʈ������ ReadAllText�޼��带 ���� ������ �о�� ��� �ּ������� ��� �о�� 
        //-- > �߰� ���� ó���� ���� �ʴ´ٸ� Ŭ���� �ν��Ͻ��� ������ �� ����
        //--->�����ϰ� ������ �ؾ� ��밡��
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
