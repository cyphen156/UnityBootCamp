using Gpm.Ui;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InfiniteScroll _infiniteScroll;

    private IInventoryService _inventoryService;

    private void Awake()
    {
        _inventoryService = CreateService();
    }

    private void Start()
    {
       
        foreach (var viewModel in _inventoryService.UnequippedItems)
        {
            Sprite gradeBackgroundSprite = Resources.Load<Sprite>($"Textures/{viewModel.GradeSpritePath}");
            Sprite itemIconSprite = Resources.Load<Sprite>($"Textures/{viewModel.ItemIconPath}");

            var slotData = new InventoryItemSlotData(gradeBackgroundSprite, itemIconSprite, viewModel.Quantity);

            _infiniteScroll.InsertData(slotData);
        }
    }

    private IInventoryService CreateService()
    {
        string path = Path.Combine(Application.persistentDataPath, "UserInventoryData.json");

        IUserInventoryDataRepository repo = new UserInventoryDataRepository(path);
        IItemRepository itemRepo = new JsonItemRepository();

        return new InventoryService(repo, itemRepo);
    }
}
