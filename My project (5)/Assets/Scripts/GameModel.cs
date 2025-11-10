using System;
using System.Collections.Generic;
using UnityEngine;

public class GameModel : IGameModel
{
    public GameSessionData GameData { get; private set; }
    public event Action OnDataChanged;

    private IDataService _dataService;

    public GameModel(IDataService dataService)
    {
        _dataService = dataService;
        LoadGame();
    }

    public void SaveGame()
    {
        _dataService.SaveData(GameData);
        OnDataChanged?.Invoke();
    }

    public void LoadGame()
    {
        GameData = _dataService.LoadData<GameSessionData>();
        if (GameData == null)
        {
            CreateNewGame();
        }
    }

    public void CreateNewGame()
    {
        GameData = new GameSessionData
        {
            playerData = new PlayerData
            {
                position = Vector3.zero,
                health = 100f,
                maxHealth = 100f,
                inventory = new InventorySlot[20],
                selectedWeaponAmmo = 30
            },
            monstersData = new List<MonsterData>(),
            worldItems = new List<WorldItemData>()
        };

        for (int i = 0; i < GameData.playerData.inventory.Length; i++)
        {
            GameData.playerData.inventory[i] = new InventorySlot();
        }

        SaveGame();
    }
}
