using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelector : MonoBehaviour
{
    public ITargetable currentlyTargeted;

    public void SelectTarget(ITargetable target)
    {
        currentlyTargeted = target;
        Debug.Log("Target selected: " + currentlyTargeted);
    }

    public void DeSelectTarget()
    {
        currentlyTargeted = null;
        Debug.Log("TargetDeselected");
    }
}
