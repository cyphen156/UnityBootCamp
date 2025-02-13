using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameStartConfig", menuName = "Homework/Dialog/Game Start Config")]
public class GameStartConfig : ScriptableObject
{
    public List<string> startDialog;

    public void AddData(string data)
    {
        startDialog.Add(data);
    }

    public void clearData()
    {
        startDialog.Clear();
    }
}
