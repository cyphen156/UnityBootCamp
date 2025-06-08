using UnityEngine;

public class ButtonTest : MonoBehaviour
{
    public void ButtonClick()
    {
        Debug.Log("Hell");
    }

    public void ToggleClick(bool isOn)
    {
        if (isOn)
        {
            Debug.Log($"Hell {isOn}");
        }
    }
}
