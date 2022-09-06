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
    private TimeManager _timeManager;


    public void Fire(Vector3 direction, CharacterFacade shooterFacade, List<Modifier> rangedWeaponModifiers,
        TimeManager timeManager)
    {
        _timeManager = timeManager;
        _facade = shooterFacade;
        _direction = direction;
        canMove = true;
        _modifiers = rangedWeaponModifiers;
    }

    private void Update()
    {
        if (canMove)
        {
            var delta = _timeManager.GetDeltaTime(this);
            transform.Translate(_direction * speed * delta);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (_facade.GameManager.GameMode == GameMode.TurnBasedFight) return;
        if (other.TryGetComponent(out Damageable damageable))
        {
            damageable.TakeDamage(_facade, _modifiers);
        }
    }
}