using UnityEngine;
using Zenject;

public class CharacterFacade : MonoBehaviour
{
    [SerializeField] private Character character;
    
    private PlayerTurn _playerTurn;
    private AiTurn _aiTurn;


    [Inject]
    public void Construct(PlayerTurn playerTurn, AiTurn aiTurn)
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
        character.InitializeStats(new Character.InitializationArguments()
        {
            caller = this,
            playerTurn = _playerTurn,
            aiTurn = _aiTurn
        });
    }
    
    public class Factory : PlaceholderFactory<CharacterFacade>
    {
    }


}