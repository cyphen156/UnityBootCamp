using System;
using System.Text;

public enum ItemType
{
    None,
    Weapon,
    Sheild,
    ChestArmor,
    Gloves,
    Boots,
    Accessory
}

public enum ItemGrade
{
    None,
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}

// 우리 게임에서 사용할 아이템 객체 => 도메인 객체 / 엔터티(Entity)
public sealed class Item
{
    private int _id;
    private string _name;
    private ItemType _type;
    private ItemGrade _grade;
    private int _atk;
    private int _def;

    public int Id => _id;
    public string GradePath => $"{_grade}";
    public string IconPath
    {
        get
        {
            StringBuilder sb = new StringBuilder(_id.ToString());
            sb[1] = '1';
            return sb.ToString();
        }
    }

    public Item(int id, string name, int atk, int def)
    {
        _id = id;
        _name = name;
        _atk = atk;
        _def = def;

        _type = GetType(id);
        _grade = GetGrade(id);
    }

    public override string ToString()
    {
        return $"Item(id: {_id}, name: {_name}, type: {_type}, grade: {_grade}, atk: {_atk}, def: {_def})";
    }

    public static int GetRandomId()
    {
        // 아이템 ID 부터 생성한다
        Random rand = new Random();

        return rand.Next(10000, 100000);
    }
    private ItemType GetType(int id)
    {
        int value = id / 10000;
        switch (value)
        {
            case 1:
                return ItemType.Weapon;
            case 2:
                return ItemType.Sheild;
            case 3:
                return ItemType.ChestArmor;
            case 4:
                return ItemType.Gloves;
            case 5:
                return ItemType.Boots;
            case 6:
                return ItemType.Accessory;
            default:
                return ItemType.None;
        }
    }

    private ItemGrade GetGrade(int id)
    {
        int value = id % 10000 / 1000;
        switch (value)
        {
            case 1:
                return ItemGrade.Common;
            case 2:
                return ItemGrade.Uncommon;
            case 3:
                return ItemGrade.Rare;
            case 4:
                return ItemGrade.Epic;
            case 5:
                return ItemGrade.Legendary;
            default:
                return ItemGrade.None;
        }
    }
}