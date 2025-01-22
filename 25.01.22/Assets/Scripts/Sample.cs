using System.Collections;
using UnityEngine;

public enum Rainbow
{
    RED,
    ORANGE,
    YELLOW,
    GREEN,
    BLUE,
    INDIGO,
    VIOLET
}

[AddComponentMenu("ø¿¿∂≈√/Sample")]
public class Sample : MonoBehaviour
{
    public bool canJump;
    public ArrayList fruitBasket;
    public uint money;
    [Range(1, 99)] public int FieldofView;
    public Rainbow rainbow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
