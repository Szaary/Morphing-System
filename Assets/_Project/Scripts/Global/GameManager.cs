using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event Action<GameMode> GameModeChanged;

    [SerializeField] private GameMode gameMode;
    public GameMode GameMode => gameMode;

    
    public void SetGameMode(GameMode newMode)
    {
        if (gameMode == newMode) return;
        
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
