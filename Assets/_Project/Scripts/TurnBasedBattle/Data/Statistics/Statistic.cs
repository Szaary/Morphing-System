using System;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "STA_", menuName = "Statistics/Statistic")]
public class Statistic : ScriptableObject, IEquatable<Statistic>
{
    /// <summary>
    /// Modifier, Current
    /// </summary>
    public event Action<float, float> OnValueChanged;

    public BaseStatistic baseStatistic;
    public float maxValue;
    public float minValue;

    [SerializeField] private float currentValue;


    public float CurrentValue
    {
        get => currentValue;
        private set => currentValue = value;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (Application.isPlaying) return;

        if (maxValue < minValue)
        {
            maxValue = minValue;
        }

        if (minValue > maxValue)
        {
            minValue = maxValue;
        }

        if (CurrentValue > maxValue)
        {
            CurrentValue = maxValue;
        }
        else if (CurrentValue < minValue)
        {
            CurrentValue = minValue;
        }
    }
#endif

    public Result Add(float modifier)
    {
        if (modifier < 0) return Result.NegativeModifier;

        if (CurrentValue + modifier > maxValue)
        {
            CurrentValue = maxValue;
            OnValueChanged?.Invoke(modifier, currentValue);
            return Result.AboveMax;
        }
        else if (CurrentValue + modifier < minValue)
        {
            CurrentValue = minValue;
            OnValueChanged?.Invoke(modifier, currentValue);
            return Result.BelowMin;
        }
        else
        {
            CurrentValue += modifier;
            OnValueChanged?.Invoke(modifier, currentValue);
            return Result.Success;
        }
    }

    public Result Subtract(float modifier)
    {
        if (modifier > 0) return Result.PositiveModifier;

        if (CurrentValue - modifier > maxValue)
        {
            CurrentValue = maxValue;
            OnValueChanged?.Invoke(modifier, currentValue);
            return Result.AboveMax;
        }
        else if (CurrentValue - modifier < minValue)
        {
            CurrentValue = minValue;
            OnValueChanged?.Invoke(modifier, currentValue);
            return Result.BelowMin;
        }
        else
        {
            CurrentValue -= modifier;
            OnValueChanged?.Invoke(modifier, currentValue);
            return Result.Success;
        }
    }


    public bool Equals(Statistic other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Equals(baseStatistic, other.baseStatistic);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Statistic) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), baseStatistic);
    }

    public static bool operator ==(Statistic left, Statistic right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Statistic left, Statistic right)
    {
        return !Equals(left, right);
    }
}