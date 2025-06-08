using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

public class UserInventoryDataViewModel
{
    public string GradeSpritePath { get; }
    public string ItemIconPath { get; }
    public int Quantity { get; }

    public UserInventoryDataViewModel(string gradeSpritePath, string itemIconPath, int quantity)
    {
        GradeSpritePath = gradeSpritePath;
        ItemIconPath = itemIconPath;
        Quantity = quantity;
    }
}

public class EquipSlotViewModel
{
    public string GradeSpritePath { get; }
    public string ItemIconPath { get; }

    public EquipSlotViewModel(string gradeSpritePath, string itemIconPath)
    {
        GradeSpritePath = gradeSpritePath;
        ItemIconPath = itemIconPath;
    }
}

public interface IInventoryService
{
    IReadOnlyCollection<UserInventoryDataViewModel> UnequippedItems { get; }
}


/// <summary>
/// 인벤토리의 기능
/// </summary>
public class InventoryService : IInventoryService
{
    private IUserInventoryDataRepository _inventoryDataRepository;
    private IItemRepository _itemRepository;
    private UserInventory _userInventory;

    public InventoryService(IUserInventoryDataRepository inventoryDataRepository, IItemRepository itemRepository)
    {
        _inventoryDataRepository = inventoryDataRepository;
        _itemRepository = itemRepository;

        // TODO: _inventory 초기화 필요
        //_inventory = _inventoryDataRepository.Load();
    }

    public IReadOnlyCollection<UserInventoryDataViewModel> UnequippedItems => _userInventory.UnequippedItems
        .Select(userItem =>
        {
            var item = _itemRepository.FindBy(userItem.ItemId);
            // userInventoryData -> UserInventoryDataViewModel
            var viewModel = new UserInventoryDataViewModel(
                item.GradePath, item.IconPath, userItem.Quantity);

            return viewModel;
        }).ToList();

    public void AcquireRandomItem()
    {
        // 대충 호출 될 때 마다 랜덤하게 인벤토리에 아이템을 추가한다.
        int randomItemId = Item.GetRandomId();

         UserInventoryData.Acquire(randomItemId);

        _userInventory.AcquireItem(item);
        // 뽑은 아이템을 인벤토리에 추가한다.
    }
}