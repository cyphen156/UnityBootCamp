using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [SerializeField] AudioMixer AudioMixer;

    // UI
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider BGMSlider;
    [SerializeField] Slider SFXSlider;

    // slider Joint 2D() :: RigidBody ������ �޴� GameObject�� ���� ���� �̲������� ����
    // �̴��̹� �����
    private void Awake()
    {
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        BGMSlider.onValueChanged.AddListener(SetBGMVolume);
        SFXSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    // Mathf(float�� ���� ��� --> Deg2Rad == Degree to Radian)
    // --> PI / 180
    // Mathf.Rad2Deg
    // Mathf.Abs ==> Absolute(A) |A|
    // Mathf.Atan ==> ArcTangent
    // Mathf.Ceil ==> �Ҽ��� �ø���
    // Mathf.Clamp(Value, min, max) ==> ���հ����� ����
    // Mathf.Log10 ==> ��� �α�

    // IMGUI : �����

    // UGUI : �Ϲ����� UI

    // UIElements : ������ UI

    // �̹����� ����� �ͼ��� ������ �ִ� �ִ� ~ �ּڰ� ������ �����
    void SetMasterVolume(float volume)
    {
        AudioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }
    void SetBGMVolume(float volume)
    {
        AudioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    }
    void SetSFXVolume(float volume)
    {
        AudioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }
}
