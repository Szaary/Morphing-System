using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 20;

    private bool canMove;
    private Vector3 _direction;

    private CharacterFacade _shooter;
    private List<Modifier> _modifiers;

    private TimeManager _timeManager;
    private GameManager _gameManager;

    [Inject]
    public void Construct(TimeManager timeManager, GameManager gameManager)
    {
        _timeManager = timeManager;
        _gameManager = gameManager;
    }


    private void Reset(Vector3 position, Vector3 direction, CharacterFacade facade, List<Modifier> modifiers)
    {
        _shooter = facade;
        _direction = direction;
        canMove = true;
        _modifiers = modifiers;
        
        transform.rotation = Quaternion.identity;
        transform.position = position;
        canMove = true;
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
        if (_gameManager.GameMode == GameMode.TurnBasedFight) return;
        if (other.TryGetComponent(out Damageable damageable))
        {
            damageable.TakeDamage(_shooter, _modifiers);
        }
    }


    public class Pool : MonoMemoryPool<Vector3, Vector3, CharacterFacade, List<Modifier>, Projectile>
    {
        protected override void Reinitialize(Vector3 position, Vector3 direction, CharacterFacade facade,
            List<Modifier> modifiers, Projectile pooled)
        {
            //  base.Reinitialize(p1, p2, p3, p4, item);
            pooled.Reset(position, direction, facade, modifiers);
        }
    }
}