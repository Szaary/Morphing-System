using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AA_", menuName = "Abilities/Active Ability/Attack")]
public class Attack : Active, IModifyStats
{
    [Header("Statistics")]
    [SerializeField] private BaseStatistic statisticToModify;
    [SerializeField] private Modifier modifier;
    [SerializeField] private List<float> amounts;

    public int actions;
    
    public List<float> Amounts { get; set; }
    public BaseStatistic StatisticToModify { get; set; }
    public Modifier Modifier { get; set; }
}

