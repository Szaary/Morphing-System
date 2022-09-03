using UnityEngine;

public abstract class RealtimeSubscriber : MonoBehaviour, ICharacterSystem
{
    private float _elapsedTime;
    private const float CycleTime = 3;

    protected CharacterFacade Facade;

    public void Initialize(CharacterFacade characterFacade)
    {
        Facade = characterFacade;
        Facade.CharacterSystems.Add(this);
    }

    
    private void Update()
    {
        var delta = Facade.TimeManager.GetDeltaTime(this);
            
        _elapsedTime += delta;
        if (_elapsedTime >= CycleTime)
        {
            _elapsedTime = 0;
            OnEnter();
        }
        else if (_elapsedTime == 0)
        {
            OnExit();
        }
        else
        {
            Tick();
        }
    }

    public abstract Result Tick();
    public abstract Result OnEnter();
    public abstract Result OnExit();
    public abstract void Disable();
    
}