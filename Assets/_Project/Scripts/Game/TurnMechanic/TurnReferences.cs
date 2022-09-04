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

    public bool ShouldWork(CharacterFacade facade, bool isOpposite)
    {
        if (facade.Alignment.IsPlayer)
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