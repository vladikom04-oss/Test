using System;
using UnityEngine;

public interface IPlayerViewModel
{
    event Action<Vector2> OnMove;
    event Action OnShoot;
    event Action OnDied;
    event Action<int> OnItemSelected;
    event Action<int> OnItemRemoved;
    public event Action<int> OnInventoryChanged;


    PlayerData PlayerData { get; }
    void Move(Vector2 direction);
    void Shoot();
    void Die();
    void PickupItem(string itemId);
    void RemoveItem(int slotIndex);
}