using System;
using UnityEngine;

[CreateAssetMenu]
[Serializable]
public class AudioEvent : ScriptableObject
{
    public event Action<string> OnPlay;

    public void Play(string name)
    {
        if (OnPlay != null)
        {
            OnPlay.Invoke(name);
        }
    }
}
