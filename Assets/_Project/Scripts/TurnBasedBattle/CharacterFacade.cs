using UnityEngine;
using Zenject;

public class CharacterFacade : MonoBehaviour, ITargetable
{
    [SerializeField] private Character character;
    [SerializeField] private TurnController turnController;
    
    private PlayerTurn _playerTurn;
    private AiTurn _aiTurn;


    [Inject]
    public void Construct(PlayerTurn playerTurn, 
        AiTurn aiTurn)
    {
        _playerTurn = playerTurn;
        _aiTurn = aiTurn;
    }
    
    public void SetCharacter(Character character)
    {
        this.character = character;
    }

    private void Start()
    {
        var arguments = new Character.InitializationArguments()
        {
            caller = this,
            playerTurn = _playerTurn,
            aiTurn = _aiTurn
        };
        
        character.InitializeStats(arguments);
        turnController.InitializeStrategy(arguments, character);
    }
    
    public class Factory : PlaceholderFactory<CharacterFacade>
    {
    }
}