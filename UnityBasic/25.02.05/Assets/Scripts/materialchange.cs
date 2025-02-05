using UnityEngine;

public class materialchange : MonoBehaviour
{
    public Material material;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RenderSettings.skybox = material;
            
        }
    }
}
