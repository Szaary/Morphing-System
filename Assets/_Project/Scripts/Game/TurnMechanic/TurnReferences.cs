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
        // SHOULD WORK in turn
        // is a player - oposite enemy turn -> if false (should work in player turn)
        
        // if in enemy turn for player
        
        if (facade.Alignment.IsPlayer)
        {
            var should = StateMachine.GetCurrentState() == TurnState.PlayerTurn; // will be not working
            if (isOpposite) return !should; //  is oposite then will be working
            return should; //if is ok
        }
        else
        {
            var should = StateMachine.GetCurrentState() == TurnState.AiTurn;
            if (isOpposite) return !should;
            return should;
        }
    }
}