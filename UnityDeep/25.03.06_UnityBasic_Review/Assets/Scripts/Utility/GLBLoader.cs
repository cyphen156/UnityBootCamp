using UnityEngine;
using Siccity.GLTFUtility;

public class GLBLoader : MonoBehaviour
{
    public string filePath = "";

    void Start()
    {
        GameObject imported = Importer.LoadFromFile(filePath);
        if (imported != null)
            imported.transform.position = gameObject.transform.position;
    }
}
