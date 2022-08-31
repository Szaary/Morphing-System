using System;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] protected CharacterFacade Facade;
 
    public void TakeDamage(CharacterFacade shooter, List<Modifier> modifiers)
    {
        Facade.Modify(shooter, modifiers);
    }
}