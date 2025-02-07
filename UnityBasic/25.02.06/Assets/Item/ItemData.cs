using System;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    [TextArea] public string itemName = "아이템 이름을 입력해주세요";
    [TextArea] public string itemDescription = "아이템 설명을 입력해주세요";

    public ItemData() { }

    public ItemData(string itemName) 
    { 
        this.itemName = itemName;
    }

    public ItemData(string itemName, string itemDescription)
    {
        this.itemName = itemName;
        this.itemDescription = itemDescription;
    }
    public string GetItemData() => $"{itemName}, {itemDescription}";

    public static ItemData SetItemData(string data)
    {
        string[] data_values = data.Split(", ");
        return new ItemData(data_values[0], data_values[1]);
    }
}
