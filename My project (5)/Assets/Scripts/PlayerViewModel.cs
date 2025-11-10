using System;
using UnityEngine;

public class PlayerViewModel : IPlayerViewModel
{
    public event Action<Vector2> OnMove;
    public event Action OnShoot;
    public event Action OnDied;
    public event Action<int> OnItemSelected;
    public event Action<int> OnItemRemoved;
    public event Action<int> OnInventoryChanged;

    public PlayerData PlayerData { get; private set; }

    private IGameModel _gameModel;

    public PlayerViewModel(IGameModel gameModel)
    {
        _gameModel = gameModel;
        PlayerData = _gameModel.GameData.playerData;
    }

    public void Move(Vector2 direction)
    {
        OnMove?.Invoke(direction);
    }

    public void Shoot()
    {
        if (PlayerData.selectedWeaponAmmo > 0)
        {
            PlayerData.selectedWeaponAmmo--;
            OnShoot?.Invoke();
        }
    }

    public void PickupItem(string itemId)
    {
        int slot = FindSlot(itemId);
        if (slot != -1)
        {
            PlayerData.inventory[slot].itemId = itemId;
            PlayerData.inventory[slot].quantity += 1;
            OnInventoryChanged?.Invoke(slot);
        }
    }

    public void RemoveItem(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < PlayerData.inventory.Length)
        {
            PlayerData.inventory[slotIndex].itemId = null;
            PlayerData.inventory[slotIndex].quantity = 0;
            OnItemRemoved?.Invoke(slotIndex);
        }
    }

    private int FindSlot(string itemId)
    {
        for (int i = 0; i < PlayerData.inventory.Length; i++)
        {
            if (PlayerData.inventory[i].itemId == itemId)
            {
                return i;
            }
            if (PlayerData.inventory[i].isEmpty)
                return i;
        }
        return -1;
    }

    public void Die()
    {
        OnDied?.Invoke();
    }
}
