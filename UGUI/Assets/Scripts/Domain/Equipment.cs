using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 장착을 관리한다.
/// 서비스에 의해서 사용 된다.
/// </summary>
public class Equipment
{
    private Dictionary<EquipSlotType, EquipSlot>_equipSlots;

    public Equipment()
    {
        _equipSlots = new Dictionary<EquipSlotType, EquipSlot>();
        _equipSlots.Clear();
        _equipSlots[EquipSlotType.Weapon] = new EquipSlot(EquipSlotType.Weapon);
        _equipSlots[EquipSlotType.Sheild] = new EquipSlot(EquipSlotType.Sheild);
        _equipSlots[EquipSlotType.ChestArmor] = new EquipSlot(EquipSlotType.ChestArmor);
        _equipSlots[EquipSlotType.Gloves] = new EquipSlot(EquipSlotType.Gloves);
        _equipSlots[EquipSlotType.Boots] = new EquipSlot(EquipSlotType.Boots);
        _equipSlots[EquipSlotType.Accessory] = new EquipSlot(EquipSlotType.Accessory);
    }

    IReadOnlyDictionary<EquipSlotType, UserInventoryData> _inventoryData;
    public IReadOnlyCollection<UserInventoryData> EquippedItems
    {
        get
        {
            return _equipSlots.Where(kvp => kvp.Value.IsEquipped).ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.EquipItem);
        }

    }
    public void Equip(EquipSlotType type, UserInventoryData item)
    {
        _equipSlots[GetIndexFrom(type)].Equip(item);
    }

    public void Unequip(EquipSlotType type)
    {
        _equipSlots[GetIndexFrom(type)].Unequip();
    }

    private int GetIndexFrom(EquipSlotType type)
    {
        return (int)type;
    }
}