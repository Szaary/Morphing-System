using UnityEngine;

public abstract class RealtimeSubscriber : MonoBehaviour
{
    private float _elapsedTime;
    private const float CycleTime = 3;

    protected CharacterFacade Facade;

    public void Initialize(CharacterFacade characterFacade)
    {
        Facade = characterFacade;
    }

    
    private void Update()
    {
        var delta = Facade.timeManager.GetDeltaTime(this);
            
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
}