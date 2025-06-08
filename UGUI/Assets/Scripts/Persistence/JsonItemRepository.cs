using System;
using System.Collections.Generic;
using System.Text;
using TMPro.Examples;
using UnityEngine;



public interface IItemRepository
{
    IReadOnlyList<Item> FindAll();
    Item FindBy(int id);
}

// Item 관련 Persistency를 담당한다.
public class JsonItemRepository : IItemRepository
{
    private List<Item> _items;

    public JsonItemRepository()
    {
        _items = LoadItems();
    }

    // 반환 타입
    public IReadOnlyList<Item> FindAll() => _items.AsReadOnly();

    // DTO; Data Transfer Object
    // 외부와 소통하기 위한 객체. 직렬화만을 위한 객체. 데이터 전송만을 위한 객체
    [Serializable]
    class ItemModel
    {
        public int item_id;
        public string item_name;
        public int attack_power;
        public int defense;
    }

    [Serializable]
    class ItemModelList
    {
        public ItemModel[] data;
    }

    List<Item> LoadItems()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("items");
        string json = jsonFile.text;
        ItemModelList itemModelList = JsonUtility.FromJson<ItemModelList>(json);

        var items = new List<Item>();
        foreach (ItemModel itemModel in itemModelList.data)
        {
            items.Add(new Item(itemModel.item_id, itemModel.item_name, itemModel.attack_power, itemModel.defense));
        }

        return items;
    }

    public Item FindBy(int id)
    {
        return _items.Find(item => item.Id == id);
    }
}