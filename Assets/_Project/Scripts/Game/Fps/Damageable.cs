using System;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] protected BaseStatistic statistic;
    [SerializeField] protected CharacterFacade Facade;

    private Statistic _chosenStat;
    public virtual void Start()
    {
        Facade.GetStatistic(statistic, out _chosenStat);
    }

    public void TakeDamage(float damage, Vector3 vector3)
    {
        _chosenStat.Subtract(damage);
    }
}