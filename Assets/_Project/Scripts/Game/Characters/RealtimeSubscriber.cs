using UnityEngine;

public abstract class RealtimeSubscriber : MonoBehaviour
{
    private float _elapsedTime;
    private const float CycleTime = 3;

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
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