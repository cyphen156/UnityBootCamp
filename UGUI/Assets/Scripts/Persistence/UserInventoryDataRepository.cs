// Repository : 객체의 퍼시스턴시
// ㄴ 영속성 메모리에 객체를 저장하거나
// ㄴ 영속성 메모리로부터 객체를 복원한다.

using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
class UserInventoryDataModel
{
    public long serial_number;
    public int item_id;
    public int quantity;
    public bool is_equipped;

    public static UserInventoryDataModel From(UserInventoryData data)
    {
        return new UserInventoryDataModel()
        {
            serial_number = data.SerialNumber,
            item_id = data.ItemId
        };
    }

    public static UserInventoryData ToDomain(UserInventoryDataModel model)
    {
        return new UserInventoryData(model.serial_number, model.item_id, model.quantity);
    }
}



[Serializable]
class UserInventoryDataModelList
{
    public List<UserInventoryDataModel> data;
}


public interface IUserInventoryDataRepository
{
    Inventory Load();
    void Save(Inventory inventory);
}


public class UserInventoryDataRepository : IUserInventoryDataRepository
{
    private readonly string _path;

    // 생성자에서 할일은?
    public UserInventoryDataRepository(string path)
    {
        _path = path;
    }

    public Inventory Load()
    {
        // 새로운 계정이라면?
        if (false == File.Exists(_path))
        {
            return Inventory.CreateEmpty();
        }

        // 파일을 읽어들여 Inventory 객체를 만들어야 한다.
        string json = File.ReadAllText(_path);
        var modelList = JsonUtility.FromJson<UserInventoryDataModelList>(json);

        // UserInventoryDataModel -> UserInventoryData
        var userItems = modelList.data
            .Select(model => UserInventoryDataModel.ToDomain(model))
            .ToList();
        return Inventory.CreateWith(userItems);
    }

    public void Save(Inventory inventory)
    {
        // inventory를 파일로 저장.

        // 1. json으로 변환
        var modelList = inventory.Items
            .Select(item => UserInventoryDataModel.From(item))
            .ToList();
        var dto = new UserInventoryDataModelList()
        {
            data = modelList
        };
        var json = JsonUtility.ToJson(dto);

        // 2. 파일입출력 라이브러리 사용해서 저장
        File.WriteAllText(_path, json);
    }
}