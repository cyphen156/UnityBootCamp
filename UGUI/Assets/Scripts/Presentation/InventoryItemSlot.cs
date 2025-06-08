using Gpm.Ui;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemSlotData : InfiniteScrollData
{
    public Sprite GradeBackgroundSprite { get; }
    public Sprite ItemIconSprite { get; }
    public int Quantity { get; }

    public InventoryItemSlotData(Sprite gradeBackgroundSprite, Sprite itemIconSprite, int quantity)
    {
        GradeBackgroundSprite = gradeBackgroundSprite;
        ItemIconSprite = itemIconSprite;
        Quantity = quantity;
    }
}

public class InventoryItemSlot : InfiniteScrollItem
{
    [SerializeField] private Image _gradeBackgroud;
    [SerializeField] private Image _itemIcon;
    [SerializeField] private TextMeshProUGUI _quantityText;

    public override void UpdateData(InfiniteScrollData scrollData)
    {
        base.UpdateData(scrollData);

        InventoryItemSlotData data = scrollData as InventoryItemSlotData;

        _gradeBackgroud.sprite = data.GradeBackgroundSprite;
        _itemIcon.sprite = data.ItemIconSprite;
        _quantityText.text = data.Quantity.ToString();
    }
}
