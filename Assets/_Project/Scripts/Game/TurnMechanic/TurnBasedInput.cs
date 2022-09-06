using System;
using UnityEngine;

public class TurnBasedInput : MonoBehaviour
{
    public event Action<int> ButtonPressed;

    public void OnUse()
    {
        //if (StopPlayerAction()) return;
    }

    public void OnManageTurn()
    {
        Pressed(-1);
    }

    public void OnTop()
    {
        Pressed(0);
    }

    public void OnDown()
    {
        Pressed(1);
    }

    public void OnLeft()
    {
        Pressed(2);
    }

    public void OnRight()
    {
        Pressed(3);
    }

    public void OnUiButton(int button)
    {
        Pressed(button);
    }

    private void Pressed(int index)
    {
        ButtonPressed?.Invoke(index);
    }
}