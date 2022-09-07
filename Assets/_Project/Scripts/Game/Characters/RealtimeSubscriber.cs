using UnityEngine;

public abstract class RealtimeSubscriber : MonoBehaviour, ICharacterSystem
{
    private float waitTime = 3.0f;
    private float timer = 0.0f;
    protected TimeManager TimeManager;
    
    protected CharacterFacade Facade;

    public void Initialize(CharacterFacade characterFacade)
    {
        Facade = characterFacade;
        TimeManager = characterFacade.TimeManager;
        SubscribeToCharacterSystems();
    }
    public void SubscribeToCharacterSystems()
    {
        Facade.CharacterSystems.Add(this);
    }

    private void Update()
    {
        var delta = TimeManager.GetDeltaTime(this);
        timer += delta;
        
        if (timer > waitTime)
        {
            OnEnter();
            OnExit();
            timer = timer - waitTime;
        }
        Tick();
    }

    public abstract Result Tick();
    public abstract Result OnEnter();
    public abstract Result OnExit();
    public abstract void Disable();
}