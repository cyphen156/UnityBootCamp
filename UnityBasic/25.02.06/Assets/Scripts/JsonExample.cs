using UnityEngine;

[System.Serializable]

public class SampleData
{
    public int i;
    public float f;
    public double d;
    public string s;
    public Vector3 vec3;
    public int[] array;
    public bool b;
    public SampleData sampleData;
}
public class JsonExample : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SampleData sample = new SampleData();

        sample.i = 0;
        sample.f = 1.0f;
        sample.d = 0.0f;
        sample.s = "str";
        sample.vec3 = new Vector3(7, 5, 6);
        sample.array = new int[] { 0, 1, 2, 3, };
        sample.b = true;
        sample.sampleData = sample;

        string jsonData = JsonUtility.ToJson(sample);

        Debug.Log("Json to string\n" + jsonData);

        SampleData sampleData2 = JsonUtility.FromJson<SampleData>(jsonData);

        Debug.Log("String to Json\n" + sampleData2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
