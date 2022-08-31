using System;
using StarterAssets;
using UnityEngine;
using UnityEngine.AI;


public class CharacterModeSwitcher : MonoBehaviour
{
    [Header("Logic")] [SerializeField] private GameObject realtime;
    [SerializeField] private GameObject turnBased;


    [Header("Movement")] public NavMeshAgent agent;
    [SerializeField] private FirstPersonController fps;
    [SerializeField] private RelativeController relative;

    [SerializeField] private CharacterController controller;

    private GameManager _gameManager;
    private CharacterFacade _facade;

    public void Initialize(CharacterFacade characterFacade)
    {
        _facade = characterFacade;
        _gameManager = _facade.gameManager;
    }


    private void Start()
    {
        OnGameModeChanged(_gameManager.GameMode);
        _gameManager.GameModeChanged += OnGameModeChanged;
    }

    private void OnDestroy()
    {
        _gameManager.GameModeChanged -= OnGameModeChanged;
    }


    private void OnGameModeChanged(GameMode gameMode)
    {
        if (gameMode == GameMode.InMenu)
        {
            TurnOffCharacterControl();
            SetTurnBasedLogic(false);
            SetRealtimeLogic(false);
        }
        if (gameMode == GameMode.TurnBasedFight)
        {
            SetTurnBasedLogic(true);
            SetRealtimeLogic(false);

            TurnOffCharacterControl();
            TurnOnNavMeshControl();

            agent.SetDestination(_facade.GetPosition().transform.position);
        }
        else if (gameMode == GameMode.Fps)
        {
            SetRealtimeLogic(true);
            if (_facade.GetRealTimeStrategy() is PlayerRealTimeStrategy)
            {
                controller.enabled = true;
                fps.enabled = true;
            }
            else
            {
                TurnOffCharacterControl();
                TurnOnNavMeshControl();
            }
        }
        else if (gameMode == GameMode.Platform)
        {
            SetRealtimeLogic(true);
            if (_facade.GetRealTimeStrategy() is PlayerRealTimeStrategy)
            {
                controller.enabled = true;
                relative.enabled = true;
            }
            else
            {
                TurnOffCharacterControl();
                TurnOnNavMeshControl();
            }
        }
    }

    private void TurnOnNavMeshControl()
    {
        agent.enabled = true;
        controller.enabled = false;

        fps.enabled = false;
        relative.enabled = false;
    }

    private void TurnOffCharacterControl()
    {
        agent.enabled = false;
        controller.enabled = false;

        fps.enabled = false;
        relative.enabled = false;
    }


    private void SetTurnBasedLogic(bool isEnabled)
    {
        turnBased.SetActive(isEnabled);
    }

    private void SetRealtimeLogic(bool isEnabled)
    {
        realtime.SetActive(isEnabled);
    }
}