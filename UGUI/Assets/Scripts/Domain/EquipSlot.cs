using System;

public enum EquipSlotType
{
    Weapon,
    Sheild,
    ChestArmor,
    Gloves,
    Boots,
    Accessory
}

/// <summary>
/// 장착 슬롯을 관리한다.
/// </summary>
public class EquipSlot
{
    private EquipSlotType _type;
    private UserInventoryData? _item;

    public EquipSlot(EquipSlotType type)
    {
        _type = type;
    }

    public bool IsEquipped => _item != null;

    public void Equip(UserInventoryData item)
    {
        if (IsEquipped)
        {
            Unequip();
        }

        _item = item;
    }

    public void Unequip()
    {
        if (IsEquipped == false)
        {
            throw new InvalidOperationException($"{_type} : 장착된 장비가 없습니다.");
        }

        _item = null;
    }
}