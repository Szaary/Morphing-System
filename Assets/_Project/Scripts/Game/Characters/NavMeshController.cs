using System;
using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private FirstPersonController fpsController;
    [SerializeField] private CharacterController controller;
    
    private CharacterFacade _facade;
    private GameManager _gameManager;

    public void SetDestination(Vector3 transformPosition)
    {
        if (agent.enabled == false) agent.enabled = true;
        agent.destination = transformPosition;
    }

    public void Initialize(CharacterFacade characterFacade)
    {
        _facade = characterFacade;
        _gameManager = _facade.gameManager;
        OnGameModeChanged(_gameManager.GameMode);
        _gameManager.GameModeChanged += OnGameModeChanged;
    }

    private void OnDisable()
    {
        _gameManager.GameModeChanged -= OnGameModeChanged;
    }

    private void OnGameModeChanged(GameMode newMode)
    {
        if (newMode == GameMode.TurnBasedFight)
        {
            fpsController.enabled = false;
            controller.enabled = false;
            SetDestination(_facade.GetPosition().transform.position);
        }
        else
        {
            if (_facade.GetRealTimeStrategy() is PlayerRealTimeStrategy)
            {
                fpsController.enabled = true;
                controller.enabled = true;
                agent.enabled = false;
            }
            else
            {
                fpsController.enabled = false;
                controller.enabled = false;
            }
        }
    }
}
