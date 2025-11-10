using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Transform slotsParent;
    [SerializeField] private InventorySlotView slotPrefab;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button deleteButton;
    [SerializeField] private float slotIntervalx = 13f;
    [SerializeField] private float slotIntervaly = 20f;

    private IPlayerViewModel _playerViewModel;
    private List<InventorySlotView> _slots = new List<InventorySlotView>();
    private int _selectedSlotIndex = -1;

    public void Initialize(IPlayerViewModel playerViewModel)
    {
        _playerViewModel = playerViewModel;
        _playerViewModel.OnInventoryChanged += UpdateSlot;

        for (int i = 0; i < playerViewModel.PlayerData.inventory.Length; i++)
        {
            var slotView = Instantiate(slotPrefab, slotsParent);
            RectTransform rect = slotView.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(-39, 32);
            rect.anchoredPosition += new Vector2((i % 5) * slotIntervalx, -(i / 5) * slotIntervaly);
            slotView.Initialize(i, playerViewModel.PlayerData.inventory[i]);
            slotView.OnSlotClicked += OnSlotSelected;
            _slots.Add(slotView);
        }

        closeButton.onClick.AddListener(CloseInventory);
        deleteButton.onClick.AddListener(DeleteSelectedItem);

        inventoryPanel.SetActive(false);
    }

    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
        if (!inventoryPanel.activeInHierarchy)
        {
            _selectedSlotIndex = -1;
            deleteButton.gameObject.SetActive(false);
        }
    }

    private void CloseInventory()
    {
        inventoryPanel.SetActive(false);
        _selectedSlotIndex = -1;
        deleteButton.gameObject.SetActive(false);
    }

    private void OnSlotSelected(int slotIndex)
    {
        _selectedSlotIndex = slotIndex;
        deleteButton.gameObject.SetActive(!_playerViewModel.PlayerData.inventory[slotIndex].isEmpty);
    }

    private void DeleteSelectedItem()
    {
        if (_selectedSlotIndex != -1)
        {
            _playerViewModel.RemoveItem(_selectedSlotIndex);
            deleteButton.gameObject.SetActive(false);
            _selectedSlotIndex = -1;
        }
    }

    public void UpdateSlot(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < _slots.Count)
        {
            _slots[slotIndex].UpdateSlot();
        }
    }
}