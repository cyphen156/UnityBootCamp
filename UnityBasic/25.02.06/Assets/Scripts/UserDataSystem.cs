using UnityEngine;

public class UserDataSystem : MonoBehaviour
{
    public UserData userData;
    public UserData userData2;
    /// 1. DeleteAll()
    /// 2. DeleteKey(KeyName)
    /// 3. Get(Key)
    /// 4. set(Key, Value)// if value is already saved :: change Value
    /// 5. HasKey(Key) :: 키가 존재하는지 여부
    /// 

    private void Start()
    {
        userData2 = new UserData();
        Debug.Log("Default Constructor Called");

        userData = new UserData("Cyphen");
        Debug.Log("Custom Constructor Called");
        Debug.Log(userData.userId);
        string data_value = userData.GetData();
        //Debug.Log(data_value);
        PlayerPrefs.SetString("Data01", data_value);
        PlayerPrefs.Save();
        userData2 = UserData.SetData(data_value);

        //Debug.Log(userData);
    }
}
