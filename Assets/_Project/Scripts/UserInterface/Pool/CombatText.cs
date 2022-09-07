using System;
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

    public event Action<CombatText> endedLife;
    
    private void Reset(float value, Vector3 position)
    {
        textReference.text = value.ToString(CultureInfo.InvariantCulture);
        var x = Random.Range(0, spawnRangeAroundTarget);
        var y = Random.Range(0, spawnRangeAroundTarget);
        var z = Random.Range(0, spawnRangeAroundTarget);
        position = new Vector3(position.x+x, position.y+y, position.z+z);
        
        transform.position = position;
        var endPosition = new Vector3(position.x, position.y+2, position.z);
        var handler = transform.DOMove(endPosition, tweenDuration)
            .SetEase(Ease.InOutSine);
        handler.onComplete += OnTweenComplete;
    }

    private void OnTweenComplete()
    {
        endedLife?.Invoke(this);
    }

    public class Pool : MonoMemoryPool<float, Vector3, CombatText>
    {
        protected override void Reinitialize(float value, Vector3 position, CombatText pooled)
        {
            pooled.Reset(value, position);
        }
    }
}