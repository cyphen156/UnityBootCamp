using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioSource bgmSource;
    public AudioSource sfxSource;
    public AudioSource reloadSource;

    private Dictionary<string, AudioClip> bgmClips = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> sfxClips = new Dictionary<string, AudioClip>();

    [System.Serializable]
    public struct NamedAudioClip
    {
        public string name;
        public AudioClip clip;
    }
    
    public NamedAudioClip[] bgmClipList;
    public NamedAudioClip[] sfxClipList;
    private Coroutine currentBGMCoroutine;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudioClips();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
    void InitializeAudioClips()
    {
        foreach (var bgm in bgmClipList)
        {
            if (!bgmClips.ContainsKey(bgm.name))
            {
                bgmClips.Add(bgm.name, bgm.clip);
            }
        }
        foreach (var sfx in sfxClipList)
        {
            if (!sfxClips.ContainsKey(sfx.name))
            {
                sfxClips.Add(sfx.name, sfx.clip);
            }
        }
    }

    public void PlayBGM(string name, float fadeDuration = 1.0f, float inVolumeScale = 1.0f)
    {
        if (bgmClips.ContainsKey(name))
        {
            if (currentBGMCoroutine != null)
            {
                StopCoroutine(currentBGMCoroutine);
            }

            currentBGMCoroutine = StartCoroutine(FadeOutBGM(fadeDuration, () =>
            {
                bgmSource.clip = bgmClips[name];
                bgmSource.Play();
                currentBGMCoroutine = StartCoroutine(FadeInBGM(fadeDuration, inVolumeScale));
            }));
        }
    }

    public void PlaySfx(string name, Vector3 position)
    {
        if (sfxClips.ContainsKey(name))
        {
            AudioSource.PlayClipAtPoint(sfxClips[name], position);
        }
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void StopSFX()
    {
        sfxSource.Stop();
    }

    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = Mathf.Clamp(volume, 0, 1);
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = Mathf.Clamp(volume, 0, 1);
    }

    IEnumerator FadeOutBGM(float duration, Action onFadeComplete)
    {
        float startVolume = bgmSource.volume;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            bgmSource.volume = Mathf.Lerp(startVolume, 0f, t / duration);
            yield return null;
        }

        bgmSource.volume = 0;
        onFadeComplete?.Invoke();
    }
    IEnumerator FadeInBGM(float duration, float inVolumeScale)
    {
        float startVolume = 0f;
        bgmSource.volume = 0f;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            bgmSource.volume = Mathf.Lerp(startVolume, inVolumeScale, t / duration);
            yield return null;
        }

        bgmSource.volume = inVolumeScale;
    }

    public void PlayReload(Vector3 position)
    {
        if (sfxClips.ContainsKey("Reload"))
        {
            reloadSource.Stop(); // 기존 소리 중단
            reloadSource.clip = sfxClips["Reload"];
            reloadSource.Play();
        }
    }
}