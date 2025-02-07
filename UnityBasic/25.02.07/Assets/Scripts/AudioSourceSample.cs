using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AudioSourceSample : MonoBehaviour
{
    // inspector창에서 직접 연결하기
    public AudioSource audioSource;

    // audio Source를 자체적으로 가지고 있는 경우 사용가능
    private AudioSource ownAudioSource;

    // 씬에서 찾는 경우
    public AudioSource audioSourceSFX;

    // 오디오 클립
    public AudioClip bgm;

    // 플레이 버튼 UI
    Button playBtn;
    Text playText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ownAudioSource = GetComponent<AudioSource>();
        audioSourceSFX = GameObject.Find("SFX").GetComponent<AudioSource>();

        audioSource.clip = bgm;
        playBtn = GameObject.Find("PlayBtn").GetComponent<Button>();
        playBtn.onClick.AddListener(Play);

        //audioSource.clip = (AudioClip)Resources.Load("Audio/Explosion");
        // AudioSoruce.PlayOneShot(target) target을 한번 재생한다 
        // AudioSoruce.PlayDelayed(Time) Time초 이후에 재생하기

        //UnityWebRequest의 GetAudioClip 기능 활용
        StartCoroutine(GetAudioClip());



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Play()
    {
        Debug.Log("Play Btn Clicked");
        StartCoroutine(GetAudioClip());
    }

    IEnumerator GetAudioClip()
    {
        UnityWebRequest uwr = UnityWebRequestMultimedia.GetAudioClip(
            "file:///"
            + Application.dataPath
            + "/Audio"
            + "/BombCharge"
            + ".wav"
            , AudioType.WAV);  
        yield return uwr.SendWebRequest(); // 전달

        // 다운로드 진행하기
        var new_Clip = DownloadHandlerAudioClip.GetContent(uwr);
        audioSource.clip = new_Clip;
        audioSource.Play();

        //yield return Time.deltaTime * 5;
        //Debug.Log("!!@@##");
    }

    
}
