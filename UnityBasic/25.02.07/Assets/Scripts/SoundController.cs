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

    // slider Joint 2D() :: RigidBody 영향을 받는 GameObject가 선을 따라 미끄러지는 설정
    // 미닫이문 만들기
    private void Awake()
    {
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        BGMSlider.onValueChanged.AddListener(SetBGMVolume);
        SFXSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    // Mathf(float형 수학 계산 --> Deg2Rad == Degree to Radian)
    // --> PI / 180
    // Mathf.Rad2Deg
    // Mathf.Abs ==> Absolute(A) |A|
    // Mathf.Atan ==> ArcTangent
    // Mathf.Ceil ==> 소숫점 올림수
    // Mathf.Clamp(Value, min, max) ==> 사잇값으로 고정
    // Mathf.Log10 ==> 상용 로그

    // IMGUI : 디버그

    // UGUI : 일반적인 UI

    // UIElements : 에디터 UI

    // 이번에는 오디오 믹서가 가지고 있는 최댓값 ~ 최솟값 때문에 사용함
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
