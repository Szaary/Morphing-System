using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CombatTextMemoryPool : IInitializable, IDisposable
{
    private readonly CombatText.Pool _pool;
    private readonly ToUiEventsHandler _eventsHandler;

    private readonly List<CombatText> _pooled = new();

    public CombatTextMemoryPool(CombatText.Pool pool, ToUiEventsHandler eventsHandler)
    {
        _pool = pool;
        _eventsHandler = eventsHandler;
    }


    public void Initialize()
    {
        _eventsHandler.HealthChanged += OnHealthChanged;
    }

    public void Dispose()
    {
        _eventsHandler.HealthChanged -= OnHealthChanged;
    }

    public void Add(float value, Vector3 position)
    {
        var spawned = _pool.Spawn(value, position);
        spawned.endedLife += Remove;
        _pooled.Add(spawned);
        
    }

    private void Remove(CombatText pooled)
    {
        pooled.endedLife -= Remove;
        _pool.Despawn(pooled);
        _pooled.Remove(pooled);
    }

    public void Remove()
    {
        var pooled = _pooled[0];
        _pool.Despawn(pooled);
        _pooled.Remove(pooled);
    }


    void OnHealthChanged(HealthChanged args)
    {
        Add(args.Modifier, args.Position);
    }
}