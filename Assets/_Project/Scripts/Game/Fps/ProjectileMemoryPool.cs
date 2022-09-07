using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ProjectileMemoryPool 
{
    private readonly Projectile.Pool _pool;
    private readonly List<Projectile> _pooled = new();
    
    public ProjectileMemoryPool(Projectile.Pool pool)
    {
        _pool = pool;
    }

    public Projectile Add(Vector3 position, Vector3 direction, CharacterFacade shooterFacade, List<Modifier> modifiers)
    {
        var spawned = _pool.Spawn(position, direction, shooterFacade, modifiers);
        _pooled.Add(spawned);
        return spawned;
    }
    
    public void Remove(Projectile pooled)
    {
        _pool.Despawn(pooled);
        _pooled.Remove(pooled);
    }
}


