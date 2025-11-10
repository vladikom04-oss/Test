using System;
public interface IGameModel
{
    GameSessionData GameData { get; }
    event Action OnDataChanged;
    void SaveGame();
    void LoadGame();
    void CreateNewGame();
}