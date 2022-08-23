using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelector : MonoBehaviour
{
    public enum Direction
    {
        Top,
        Bottom,
        Left,
        Right
    }
    
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

    public void Target(Direction top)
    {
        Debug.Log("Select "+ top);
    }
}
