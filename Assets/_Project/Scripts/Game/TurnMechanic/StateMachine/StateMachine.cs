using System;
using System.Collections.Generic;
using System.Threading.Tasks;


public class StateMachine
{
    private BaseState _currentBaseState;

    private readonly Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();
    private List<Transition> _currentTransitions = new List<Transition>();
    
    private readonly List<Transition> _anyTransitions = new List<Transition>();
    private static readonly List<Transition> EmptyTransitions = new List<Transition>(0);

    public void Tick()
    {
        var transition = GetTransition();
        if (transition != null)
            SetState(transition.To);

        _currentBaseState?.Tick();
    }

    public void SetState(BaseState baseState)
    {
        if (baseState == _currentBaseState)
            return;

        _currentBaseState?.OnExit();
        _currentBaseState = baseState;

        _transitions.TryGetValue(_currentBaseState.GetType(), out _currentTransitions);
        _currentTransitions ??= EmptyTransitions;

        _currentBaseState?.OnEnter();
    }

    public void AddTransition(BaseState from, BaseState to, Func<bool> predicate)
    {
        if (_transitions.TryGetValue(from.GetType(), out var transitions) == false)
        {
            transitions = new List<Transition>();
            _transitions[from.GetType()] = transitions;
        }

        transitions.Add(new Transition(to, predicate));
    }

    public void AddAnyTransition(BaseState baseState, Func<bool> predicate)
    {
        _anyTransitions.Add(new Transition(baseState, predicate));
    }

    private class Transition
    {
        public Func<bool> Condition { get; }
        public BaseState To { get; }

        public Transition(BaseState to, Func<bool> condition)
        {
            To = to;
            Condition = condition;
        }
    }

    private Transition GetTransition()
    {
        foreach (var transition in _anyTransitions)
            if (transition.Condition())
                return transition;

        foreach (var transition in _currentTransitions)
            if (transition.Condition())
                return transition;

        return null;
    }
}