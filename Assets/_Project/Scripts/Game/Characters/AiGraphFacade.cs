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

    public void SetDestination(Vector3 destination)
    {
        Facade.movement.SetDestination(destination);
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
        enabled = false;
    }
}