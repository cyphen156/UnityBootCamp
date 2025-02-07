using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

[System.Serializable]
public class UserData
{
    public string userId;
    public string userName;
    public string userPassword;
    public string userEmail;

    // 持失切 
    // 社瑚切

    public UserData() { }
    public UserData(string userId, string userName = "", string userPassword = "123", string userEmail = "")
    {
        this.userId = userId;
        this.userName = userName;
        this.userPassword = userPassword;
        this.userEmail = userEmail;
    }

    // ArrowFunction ==>> Lambda
    public string GetData() => $"{userId}, {userName}, {userPassword}, {userEmail}";

    public static UserData SetData(string data)
    {
        string[] data_values = data.Split(", ");
        return new UserData(data_values[0], data_values[1], data_values[2], data_values[3]);
    }
}
