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

    // ����Ʈ�� ����
    private int samples = 64;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boards = GetComponentsInChildren<Board>();

        // ����� ��ȿ�� üũ
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
        // sample = FFT == ���ļ� ����, 2�� ���� ���� ǥ��
        // Max 8 Channel, general == 0
        // FFTWindow == Sampling ������ �� ���� ��


        // ���� ������ŭ �۾�
        for (int i = 0; i < boards.Length; ++i)
        {
            Vector2 size = boards[i].GetComponent<RectTransform>().rect.size;

            size.y = minBoard + (data[i] * (maxBoard - minBoard) * 3.0f); // ����ġ 3.0f

            boards[i].GetComponent<RectTransform>().sizeDelta = size;
        }
    }
}
