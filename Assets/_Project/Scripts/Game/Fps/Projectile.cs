using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Change to factory implementation and add agility modifier
    [SerializeField] private float speed = 20;

    private bool canMove;
    private Vector3 _direction;

    private CharacterFacade _facade;
    private List<Modifier> _modifiers;


    public void Fire(Vector3 direction, CharacterFacade shooterFacade, List<Modifier> rangedWeaponModifiers)
    {
        _facade = shooterFacade;
        _direction = direction;
        canMove = true;
        _modifiers = rangedWeaponModifiers;
    }

    private void Update()
    {
        if (canMove) transform.Translate(_direction * speed * Time.deltaTime);
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent == null)
        {
            return;
        }
        if (other.transform.parent.TryGetComponent(out Damageable damageable))
        {
            damageable.TakeDamage(_facade, _modifiers);
        }
    }
}