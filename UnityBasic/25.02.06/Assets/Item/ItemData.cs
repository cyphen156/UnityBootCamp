using System;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    [TextArea] public string itemName = "������ �̸��� �Է����ּ���";
    [TextArea] public string itemDescription = "������ ������ �Է����ּ���";

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
