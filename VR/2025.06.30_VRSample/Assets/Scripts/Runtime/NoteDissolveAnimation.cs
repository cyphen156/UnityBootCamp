using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteDissolveAnimation : MonoBehaviour
{
    readonly int DISSOLVE_ID = Shader.PropertyToID("_Dissolve");
    [SerializeField] float _duration = 2.0f;
    MaterialPropertyBlock _block;

    private void Awake()
    {
        _block = new MaterialPropertyBlock();
    }

    public void PlayDissolve(Renderer renderer, Action onFinish = null)
    {
        StartCoroutine(C_Dissolve(renderer, onFinish));
    }
    
    IEnumerator C_Dissolve(Renderer renderer, Action onFinish)
    {
        float animationSpeed = 1f / _duration;
        float dissolve = 0f;
        float elapsedTime = 0f;

        while (dissolve < 1f)
        {
            dissolve = elapsedTime * animationSpeed;

            elapsedTime += Time.deltaTime;

            renderer.GetPropertyBlock(_block);
            _block.SetFloat(DISSOLVE_ID, dissolve);
            renderer.SetPropertyBlock(_block);
            //renderer.material.SetFloat(DISSOLVE_ID, dissolve); 이렇게쓰면 머티리얼 인스턴스가 한개 더 생김 (메모리낭비)
            yield return null;
        }

        onFinish?.Invoke();
    }
}
