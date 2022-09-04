using Unity.VisualScripting;
using UnityEngine;

public class GraphDataEndPoint : MonoBehaviour, ICharacterSystem
{
    [SerializeField] private ScriptGraphAsset asset;
    private CharacterFacade _facade;
    private CharactersLibrary _library;
    private CharacterFacade _player;
    private bool _playerSet;
    private NavMeshAgentMovement _agent;
    private RangedWeaponController _weapon;

    
   
    
    public void Initialize(CharacterFacade characterFacade)
    {
        _facade = characterFacade;
        _facade.CharacterSystems.Add(this);
        _library = _facade.Library;
        _agent = _facade.movement.navMeshAgentMovement;
        _weapon = _facade.rangedWeaponController;
        
        _library.GetControlledCharacter(out _player);
        _library.ControlledCharacterChanged += OnControlledCharacterChanged;
        
        
    }
    
    private void OnControlledCharacterChanged(CharacterFacade player)
    {
        if (player is not null) _playerSet = true;
        _player = player;
    }
    
    private void OnDestroy()
    {
        _library.ControlledCharacterChanged -= OnControlledCharacterChanged;
    }

    public void Disable()
    {
        enabled = false;
    }
}
