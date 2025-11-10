using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotView : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private Button slotButton;

    private ItemData itemData;
    public event Action<int> OnSlotClicked;

    private int _slotIndex;
    private InventorySlot _slotData;

    public void Initialize(int slotIndex, InventorySlot slotData)
    {
        _slotIndex = slotIndex;
        _slotData = slotData;

        slotButton.onClick.AddListener(() => OnSlotClicked?.Invoke(_slotIndex));
        UpdateSlot();
    }

    public void UpdateSlot()
    {

        if (_slotData.isEmpty)
        {
            itemIcon.sprite = null;
            itemIcon.color = Color.clear;
            quantityText.text = "";
        }
        else
        {
            if (itemData == null)
            {
                itemData = Resources.Load<ItemData>($"Items/{_slotData.itemId}");
                if (itemData != null)
                {
                    Sprite icon = Resources.Load<Sprite>(itemData.iconPath);
                    itemIcon.sprite = icon;
                    itemIcon.color = Color.white;
                }
            }   
            quantityText.text = _slotData.quantity > 1 ? _slotData.quantity.ToString() : "";
        }
    }
}