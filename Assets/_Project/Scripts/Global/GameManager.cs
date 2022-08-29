using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event Action<GameMode> GameModeChanged;

    [SerializeField] private GameMode gameMode;
    public GameMode GameMode => gameMode;

    
    public void ChangeGameMode(GameMode newMode)
    {
        Debug.Log("Game Mode Changed");
        gameMode = newMode;
        GameModeChanged?.Invoke(gameMode);
    }

    

    [Serializable]
    public class Settings
    {
        public int level;
    }
}
