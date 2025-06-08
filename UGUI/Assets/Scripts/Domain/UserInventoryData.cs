using System;

/// <summary>
/// 유저의 인벤토리 데이터
/// </summary>
public class UserInventoryData
{
    private static readonly Random random = new Random();

    public long SerialNumber { get; }
    public int ItemId { get; }
    public int Quantity { get; }

    public static UserInventoryData Acquire(int itemId)
    {
        long serialNumber = long.Parse(DateTime.Now.ToString("yyyymmdd") + random.Next(10000).ToString("D4"));

        return new UserInventoryData(serialNumber, itemId, 1);
    }

    public UserInventoryData(long serialNumber, int itemId, int quantity)
    {
        SerialNumber = serialNumber;
        ItemId = itemId;
        Quantity = quantity;
    }

    public override string ToString()
    {
        return $"Inven Data : {SerialNumber}, {ItemId}";
    }

    public bool Equals(UserInventoryData other)
    {
        if (other == null)
        {
            return false;
        }

        return SerialNumber == other.SerialNumber;
    }
}