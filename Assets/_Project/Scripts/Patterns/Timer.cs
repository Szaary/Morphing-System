using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Timer : MonoBehaviour
{
    [SerializeField] private float waitTime = 3.0f;
    private float timer = 0.0f;
    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer > waitTime)
        {
            OnTimePassed();
            timer = timer - waitTime;
        }
    }

    public abstract void OnTimePassed();
}
