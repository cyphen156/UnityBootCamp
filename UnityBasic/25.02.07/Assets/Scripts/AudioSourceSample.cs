using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AudioSourceSample : MonoBehaviour
{
    // inspectorâ���� ���� �����ϱ�
    public AudioSource audioSource;

    // audio Source�� ��ü������ ������ �ִ� ��� ��밡��
    private AudioSource ownAudioSource;

    // ������ ã�� ���
    public AudioSource audioSourceSFX;

    // ����� Ŭ��
    public AudioClip bgm;

    // �÷��� ��ư UI
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
        // AudioSoruce.PlayOneShot(target) target�� �ѹ� ����Ѵ� 
        // AudioSoruce.PlayDelayed(Time) Time�� ���Ŀ� ����ϱ�

        //UnityWebRequest�� GetAudioClip ��� Ȱ��
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
        yield return uwr.SendWebRequest(); // ����

        // �ٿ�ε� �����ϱ�
        var new_Clip = DownloadHandlerAudioClip.GetContent(uwr);
        audioSource.clip = new_Clip;
        audioSource.Play();

        //yield return Time.deltaTime * 5;
        //Debug.Log("!!@@##");
    }

    
}
