public class TurnReferences
{
    public readonly BattleStart BattleStart;
    public readonly PlayerTurn PlayerTurn;
    public readonly AiTurn AiTurn;
    public readonly Victory Victory;
    public readonly Defeat Defeat;
    public readonly TurnStateMachine StateMachine;

    public TurnReferences(BattleStart battleStart, PlayerTurn playerTurn,
        AiTurn aiTurn, Victory victory, Defeat defeat, TurnStateMachine stateMachine
    )
    {
        BattleStart = battleStart;
        PlayerTurn = playerTurn;
        AiTurn = aiTurn;
        Victory = victory;
        Defeat = defeat;
        StateMachine = stateMachine;
    }

    public BaseState GetPlayTurn(CharacterFacade facade, bool workOnOppositeTurn)
    {
        if (workOnOppositeTurn)
        {
            if (facade.Alignment.id == 0)
                return AiTurn;
            else
                return PlayerTurn;
        }
        else
        {
            if (facade.Alignment.id == 0)
                return PlayerTurn;
            else
                return AiTurn;
        }
    }

    public bool ShouldWork(CharacterFacade facade, bool isOpposite)
    {
        if (facade.Alignment.id == 0)
        {
            var should = StateMachine.GetCurrentState() != TurnState.AiTurn;
            if (isOpposite) return !should;
            return should;
        }
        else
        {
            var should = StateMachine.GetCurrentState() != TurnState.AiTurn;
            if (isOpposite) return !should;
            return should;
        }
    }
}