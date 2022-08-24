public class TurnReferences
{
    public readonly BattleStart BattleStart;
    public readonly PlayerTurn PlayerTurn;
    public readonly AiTurn AiTurn;
    public readonly Victory Victory;
    public readonly Defeat Defeat;
    
    public TurnReferences(BattleStart battleStart, PlayerTurn playerTurn, AiTurn aiTurn, Victory victory, Defeat defeat)
    {
        BattleStart = battleStart;
        PlayerTurn = playerTurn;
        AiTurn = aiTurn;
        Victory = victory;
        Defeat = defeat;
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
}