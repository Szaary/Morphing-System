using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealtimeController : MonoBehaviour
{
    private CharacterFacade _facade;
    public void Initialize(CharacterFacade character)
    {
        _facade = character;
    }
}
