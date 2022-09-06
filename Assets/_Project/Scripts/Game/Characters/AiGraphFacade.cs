using System.Collections.Generic;
using UnityEngine;

public class AiGraphFacade : MonoBehaviour, ICharacterSystem
{
    private CharacterFacade _facade;
    private CharactersLibrary _library;
    private CharacterFacade _player;
    private NavMeshAgentMovement _agent;
    private LayerMask playerLayer;

    public float DeltaTime => _facade.TimeManager.GetDeltaTime(this);

    public CharacterFacade Facade
    {
        get => _facade;
        set => _facade = value;
    }

    public CharactersLibrary Library
    {
        get => _library;
        set => _library = value;
    }

    public CharacterFacade Player
    {
        get => _player;
        set => _player = value;
    }

    public Vector3 SpawnPosition
    {
        get => _facade.GetPosition().transform.position;
    }

    public void MoveToLocation(Vector3 position)
    {
        Facade.movement.navMeshAgentMovement.MoveToLocation(position);
    }

    public void RunToLocation(Vector3 position)
    {
        Facade.movement.navMeshAgentMovement.RunToLocation(position);
    }

    public void SprintToLocation(Vector3 position)
    {
        Facade.movement.navMeshAgentMovement.SprintToLocation(position);
    }

    public bool GetRandomPosition(Vector3 origin, float radius, LayerMask mask, out Vector3 position)
    {
        return Facade.movement.navMeshAgentMovement.RandomNavmeshLocation(origin, radius, mask, out position);
    }

    public Transform GetPlayerPosition()
    {
        //TODO Change this to support enemy units instead of player.
        return _player.transform;
    }

    public float GetAttackRange()
    {
        return _facade.rangedWeaponController.Range;
    }
    

    public Alignment Alignment => Facade.Alignment;
    public GameManager GameManager => Facade.GameManager;
    public LayerMask PlayerLayer => _player.Alignment.FactionLayerMask;


    public void Initialize(CharacterFacade characterFacade)
    {
        Facade = characterFacade;
        Facade.CharacterSystems.Add(this);
        Library = Facade.Library;

        Library.GetControlledCharacter(out _player);
        Library.ControlledCharacterChanged += OnControlledCharacterChanged;

        SubscribeToCharacterSystems();
    }

    private void OnControlledCharacterChanged(CharacterFacade player)
    {
        Player = player;
    }

    private void OnDestroy()
    {
        Library.ControlledCharacterChanged -= OnControlledCharacterChanged;
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void SubscribeToCharacterSystems()
    {
        _facade.CharacterSystems.Add(this);
    }
}