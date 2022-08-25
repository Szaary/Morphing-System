using System;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Action<GameMode> GameModeChanged;

    [SerializeField] private GameMode gameMode;
    public GameMode GameMode => gameMode;

    
    public void ChangeGameMode(GameMode newMode)
    {
        gameMode = newMode;
        GameModeChanged?.Invoke(gameMode);
    }

    

    [Serializable]
    public class Settings
    {
        public int level;
    }
}
