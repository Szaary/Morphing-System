using UnityEngine;

public class AiGraphFacade : MonoBehaviour, ICharacterSystem
{
    private CharacterFacade _facade;
    private CharactersLibrary _library;
    private CharacterFacade _player;
    private NavMeshAgentMovement _agent;
    private RangedWeaponController _weapon;
    private LayerMask playerLayer;
    
    
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

    public NavMeshAgentMovement Agent
    {
        get => _agent;
        set => _agent = value;
    }

    public RangedWeaponController Weapon
    {
        get => _weapon;
        set => _weapon = value;
    }

    public LayerMask PlayerLayer
    {
        get => playerLayer;
        private set => playerLayer = value;
    }

    public void Initialize(CharacterFacade characterFacade)
    {
        Facade = characterFacade;
        Facade.CharacterSystems.Add(this);
        Library = Facade.Library;
        Agent = Facade.movement.navMeshAgentMovement;
        Weapon = Facade.rangedWeaponController;

        Library.GetControlledCharacter(out _player);
        Library.ControlledCharacterChanged += OnControlledCharacterChanged;
    }

    private void OnControlledCharacterChanged(CharacterFacade player)
    {
        Player = player;
        PlayerLayer = _player.Alignment.FactionLayerMask;
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