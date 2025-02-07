using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioBoAudioBoardVisualizer : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioSource audioSource;
    public AudioMixer audioMixer;
    public Board[] boards;

    [SerializeField] private const float minBoard = 50.0f;
    [SerializeField] private const float maxBoard = 500.0f;

    // 스펙트럼 범위
    private int samples = 64;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boards = GetComponentsInChildren<Board>();

        // 오디오 유효성 체크
        if (!audioSource)
        {
            audioSource = new GameObject("audioSource").AddComponent<AudioSource>();
        }
        else
        {
            audioSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        }
        audioSource.clip = audioClip;
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master")[0];
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        float[] data = audioSource.GetSpectrumData(samples, 0, FFTWindow.Rectangular);
        // GetSpectrumData(samples, 0, FFTWindow.Rectangular);
        // sample = FFT == 주파수 영역, 2의 제곱 수로 표현
        // Max 8 Channel, general == 0
        // FFTWindow == Sampling 진행할 때 쓰는 값


        // 보드 갯수만큼 작업
        for (int i = 0; i < boards.Length; ++i)
        {
            Vector2 size = boards[i].GetComponent<RectTransform>().rect.size;

            size.y = minBoard + (data[i] * (maxBoard - minBoard) * 3.0f); // 보정치 3.0f

            boards[i].GetComponent<RectTransform>().sizeDelta = size;
        }
    }
}
