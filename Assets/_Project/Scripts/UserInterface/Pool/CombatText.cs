using System;
using System.Collections.Generic;
using System.Globalization;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class CombatText : MonoBehaviour
{
    [SerializeField] private TextMeshPro textReference;
    [SerializeField] private float tweenDuration = 2;
    [SerializeField] private float spawnRangeAroundTarget=0.1f;
    
    private void Reset(float value, Vector3 position)
    {
        textReference.text = value.ToString(CultureInfo.InvariantCulture);
        var x = Random.Range(0, spawnRangeAroundTarget);
        var y = Random.Range(0, spawnRangeAroundTarget);
        var z = Random.Range(0, spawnRangeAroundTarget);
        position = new Vector3(position.x+x, position.y+y, position.z+z);
        
        transform.position = position;
        var endPosition = new Vector3(position.x, position.y+2, position.z);
        var handler = transform.DOMove(endPosition, tweenDuration).SetEase(Ease.InOutSine);
        handler.onComplete += OnTweenComplete;
    }

    private void OnTweenComplete()
    {
        Destroy(gameObject);
    }

    public class Pool : MonoMemoryPool<float, Vector3, CombatText>
    {
        protected override void Reinitialize(float value, Vector3 position, CombatText pooled)
        {
            pooled.Reset(value, position);
        }
    }


}

public class CombatTextFactory : IInitializable, IDisposable
{
    private readonly CombatText.Pool _pool;
    private readonly ToUiEventsHandler _eventsHandler;

    private readonly List<CombatText> _pooled = new();

    public CombatTextFactory(CombatText.Pool pool, ToUiEventsHandler eventsHandler)
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
        _pooled.Add(_pool.Spawn(value, position));
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