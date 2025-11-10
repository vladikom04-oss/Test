using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class InventorySlot
{
    public string itemId;
    public int quantity;
    public bool isEmpty => string.IsNullOrEmpty(itemId);
}

[Serializable]
public class PlayerData
{
    public Vector2 position;
    public float health;
    public float maxHealth;
    public InventorySlot[] inventory;
    public int selectedWeaponAmmo;
}

[Serializable]
public class MonsterData
{
    public string id;
    public Vector3 position;
    public float health;
    public float maxHealth;
    public bool isAlive;
    public string lootItemId;
}

[Serializable]
public class GameSessionData
{
    public PlayerData playerData;
    public List<MonsterData> monstersData;
    public List<WorldItemData> worldItems;
}

[Serializable]
public class WorldItemData
{
    public string itemId;
    public Vector3 position;
    public bool isCollected;
}
