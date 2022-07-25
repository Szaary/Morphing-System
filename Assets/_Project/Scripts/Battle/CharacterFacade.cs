using UnityEngine;
using Zenject;

public class CharacterFacade : MonoBehaviour
{
    [SerializeField] private Character character;
    private PlayerTurn _playerTurn;


    [Inject]
    public void Construct(PlayerTurn playerTurn)
    {
        _playerTurn = playerTurn;
    }

    private void Start()
    {
        character.InitializeStats(this, _playerTurn);

        foreach (var statistic in character.battleStats.statistics)
        {
            Debug.Log(statistic.baseStatistic.statName + " " + statistic.CurrentValue);
        }
    }
}