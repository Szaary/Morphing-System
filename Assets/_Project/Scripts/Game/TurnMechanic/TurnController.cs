using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : TurnsSubscriber, IDoActions, ICharacterSystem
{
    public int ActionPoints { get; private set; }

    public bool animationEnded;
    public bool animationWorked;

    private CharacterFacade _facade;
    public TurnBasedStrategy.SelectedStrategy SelectedStrategy = new();


    public void Initialize(CharacterFacade character)
    {
        _facade = character;
        if (character.Alignment.ID == 0)
            SubscribeToState(_facade.Turns.PlayerTurn);
        else
            SubscribeToState(_facade.Turns.AiTurn);
        
        _facade.CharacterSystems.Add(this);
    }

    private void SubscribeToState(BaseState state)
    {
        SubscribeToStateChanges(state);
    }

    public override Result OnEnter()
    {
        animationEnded = false;
        animationWorked = false;
        ActionPoints = _facade.GetActionPoints();

        var unitStrategy = _facade.GetTurnBasedStrategy();
        var result = unitStrategy.SelectTactic(CreateFightState(), out var selectedStrategy);
        if (result != Result.Success)
        {
            Debug.LogError(typeof(TurnController) + " " + result);
        }

        if (unitStrategy is not PlayerTurnBasedStrategy)
        {
            StartCoroutine(WaitForAttack(selectedStrategy));
        }

        return Result.Success;
    }

    private IEnumerator WaitForAttack(TurnBasedStrategy.SelectedStrategy selectedStrategy)
    {
        var random = Random.Range(0, 0.7f);
        yield return new WaitForSeconds(random);
        SelectStrategy(selectedStrategy);
    }


    public void SelectStrategy(TurnBasedStrategy.SelectedStrategy strategy)
    {
        SelectedStrategy = strategy;
        if (strategy.selectedSkill.IsAttack()) _facade.animatorManager.Attack();
        else if (strategy.selectedSkill.IsDefensive()) _facade.animatorManager.Defensive();
    }


    public override Result Tick()
    {
        if (SelectedStrategy.selected && animationWorked)
        {
            ActivateEffect();
        }

        ManageActionPoints();

        return Result.Success;
    }

    private void ActivateEffect()
    {
        SelectedStrategy.selectedSkill.ActivateEffect(SelectedStrategy.selectTargets, SelectedStrategy.character);
        animationWorked = false;
    }

    private void ManageActionPoints()
    {
        if (animationEnded)
        {
            ActionPoints -= SelectedStrategy.selectedSkill.cost;
            animationEnded = false;
            SelectedStrategy = new TurnBasedStrategy.SelectedStrategy();
        }
    }

    public override Result OnExit()
    {
        return Result.Success;
    }

    private TurnBasedStrategy.CurrentFightState CreateFightState()
    {
        return new TurnBasedStrategy.CurrentFightState()
        {
            Character = _facade,
            Library = _facade.Library,
            Points = ActionPoints,
            TurnBasedInputManager = _facade.BasedInputManager
        };
    }


    private void OnDestroy()
    {
        UnsubscribeFromStates();
    }

    public void Disable()
    {
        UnsubscribeFromStates();
        enabled = false;
    }
}