using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerView playerView;
    [SerializeField] private JoystickView joystickView;
    [SerializeField] private InventoryView inventoryView;
    [SerializeField] private Button shootButton;
    [SerializeField] private Button inventoryButton;
    [SerializeField] private AmmoView ammoView;
    [SerializeField] private MonsterView monsterPrefab;

    private IGameModel _gameModel;
    private IPlayerViewModel _playerViewModel;
    private IDataService _dataService;

    private bool isSave;

    private void Awake()
    {
        InitializeDependencies();
        InitializeViews();
        SpawnMonsters();
    }

    private void InitializeDependencies()
    {

        _dataService = new JsonDataService();

        _gameModel = new GameModel(_dataService);

        _playerViewModel = new PlayerViewModel(_gameModel);
    }

    private void InitializeViews()
    {
        playerView.Initialize(_playerViewModel);
        ammoView.Initialize(_playerViewModel);

        joystickView.Initialize(_playerViewModel);
        inventoryView.Initialize(_playerViewModel);

        shootButton.onClick.AddListener(() => _playerViewModel.Shoot());
        inventoryButton.onClick.AddListener(() => inventoryView.ToggleInventory());

        _playerViewModel.OnItemRemoved += OnItemRemoved;
        _playerViewModel.OnDied += End;

        isSave = true;
    }

    private void End()
    {
        isSave = false;
    }

    private void SpawnMonsters()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            MonsterView monster = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);

            var monsterData = new MonsterData
            {
                id = $"monster_{i}",
                position = spawnPosition,
                health = 50f,
                maxHealth = 50f,
                isAlive = true,
                lootItemId = i.ToString()
            };

            monsterPrefab.Initialize(monsterData);
            monster.Initialize(monsterData);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        return new Vector3(Random.Range(-7f, 7f), Random.Range(-5f, 5f), 0);
    }

    private void OnItemRemoved(int slotIndex)
    {
        inventoryView.UpdateSlot(slotIndex);
    }

    private void OnApplicationQuit()
    {
        if (isSave)
        {
            _gameModel.SaveGame();
        }
    }
}