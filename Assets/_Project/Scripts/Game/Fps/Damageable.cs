using System;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    private CharacterFacade _facade;
 
    public void TakeDamage(CharacterFacade shooter, List<Modifier> modifiers)
    {
        _facade.Modify(shooter, modifiers);
    }

    private void OnValidate()
    {
        if (_facade == null)
        {
            _facade = GetComponent<CharacterFacade>();
        }
    }
}