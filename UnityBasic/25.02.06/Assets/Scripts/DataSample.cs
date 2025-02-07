using UnityEngine;

public class DataSample : MonoBehaviour
{
    public UserData userData;

    private void Start()
    {
        PlayerPrefs.SetString("ID", userData.userId);
        PlayerPrefs.SetString("UserName", userData.userName);
        PlayerPrefs.SetString("Password", userData.userPassword);
        PlayerPrefs.SetString("E-mail", userData.userEmail);


        Debug.Log("Hi");
        Debug.Log(PlayerPrefs.GetString(userData.userPassword));

        //PlayerPrefs.DeleteAll();
        Debug.Log("Bye");
    }
}
