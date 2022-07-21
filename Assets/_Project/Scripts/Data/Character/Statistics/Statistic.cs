using System;
using UnityEngine;

//[CreateAssetMenu(fileName = "STA_", menuName = "Statistics/Statistic")]
public class Statistic : ScriptableObject, IEquatable<Statistic>
{
    public BaseStatistic baseStatistic;
    public float maxValue;
    public float minValue;

    [SerializeField] private float currentValue;

    public float CurrentValue
    {
        get => currentValue;
        set
        {
            currentValue = value;
            if (currentValue < 1) currentValue = 1;
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