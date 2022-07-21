using System.Collections.Generic;
using UnityEngine;

public abstract class Modifier : ScriptableObject 
{
    public abstract void Modify(Statistic stats, List<float> modifiers);
    public abstract void UnModify(Statistic stats, List<float> modifiers);
}
