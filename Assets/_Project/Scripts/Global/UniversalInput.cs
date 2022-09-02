using ModestTree;
using UnityEngine;
using UnityEngine.InputSystem;

public class UniversalInput : MonoBehaviour
{
    public bool onTurn;
    public bool onFps;
    public bool onMenu;
    public bool onConsole;
    public bool onTpp;
    
    public void OnTurn(InputValue value)
    {
        onTurn = value.isPressed;
    }

    public void OnFps(InputValue value)
    {
        onFps = value.isPressed;
    }
    
    public void OnTpp(InputValue value)
    {
        onTpp = value.isPressed;
    }
    
    public void OnMenu(InputValue value)
    {
        onMenu = value.isPressed;
    }

    public void OnConsole(InputValue value)
    {
        onConsole = value.isPressed;
    }
}