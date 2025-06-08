using Newtonsoft.Json.Bson;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Analytics;

/// <summary>
/// 유저가 소유한 아이템을 관리한다.
/// </summary>
public class Inventory
{
    private List<UserInventoryData> _items;

    public static Inventory CreateWith(List<UserInventoryData> items)
    {
        return new Inventory(items);
    }

    public static Inventory CreateEmpty()
    {
        return new Inventory(new List<UserInventoryData>());
    }

    private Inventory(List<UserInventoryData> items)
    {
        _items = items;
    }

    public IReadOnlyCollection<UserInventoryData> Items => _items.AsReadOnly();

    public bool Contains(long serialNumber)
    {
        var found = _items.Find(item => item.SerialNumber == serialNumber);

        if (found != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddItem(UserInventoryData item)
    {
        _items.Add(item);
    }
}