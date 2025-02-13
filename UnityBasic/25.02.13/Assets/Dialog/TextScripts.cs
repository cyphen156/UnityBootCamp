using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Textscripts", menuName = "Scriptable Objects/Textscripts")]
public class TextScripts : ScriptableObject
{
    public List<string> text_Scripts;

    public void AddBehavior(string str)
    {
        text_Scripts.Add(str);
    }
    public string GetBehavior(int idx)
    {
        return text_Scripts[idx];
    }

}
