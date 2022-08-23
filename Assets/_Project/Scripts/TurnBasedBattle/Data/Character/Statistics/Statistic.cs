using System;
using UnityEngine;

[CreateAssetMenu(fileName = "STA_", menuName = "Statistics/Statistic")]
public class Statistic : ScriptableObject, IEquatable<Statistic>
{
    public event Action<float> OnValueChanged;
    public enum Result
    {
        Success,
        AboveMax,
        BelowMin
    } 
        
        
    public BaseStatistic baseStatistic;
    public float maxValue;
    public float minValue;

    [SerializeField] private float currentValue;
    
    
    public float CurrentValue
    {
        get => currentValue;
        private set
        {
            currentValue = value;
            OnValueChanged?.Invoke(currentValue);
        }
    }
    
    public void Initialize(Statistic stat)
    {
        maxValue = stat.maxValue;
        minValue = stat.minValue;
        CurrentValue = stat.CurrentValue;
        
        baseStatistic = stat.baseStatistic;
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
        if(CurrentValue + modifier > maxValue)
        {
            CurrentValue=maxValue;
            return Result.AboveMax;
        }
        else if(CurrentValue + modifier < minValue)
        {
            CurrentValue = minValue;
            return Result.BelowMin;
        }
        else
        {
            CurrentValue += modifier;
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