using System.Drawing;
using UnityEngine;


public class Test : MonoBehaviour
{
    int point = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        point = SingleTon.Instance.point;
        SingleTon.GetInst().PointPlus();
    }
}
