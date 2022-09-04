using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class RealtimeController : MonoBehaviour, ICharacterSystem
{
    private Vector3 destinationPoint;
    
    [SerializeField] private float sightRange, attackRange, timeBetweenAttacks, wanderRange;
    private bool playerInSightRange, playerInAttackRange, attacked, isDestinationSet;

    [SerializeField] private LayerMask groundLayerMask;
    
    private CharacterFacade _facade;
    private CharactersLibrary _library;
    private CharacterFacade _player;
    private bool _playerSet;
    private NavMeshAgentMovement _agent;
    private RangedWeaponController _weapon;


    public void Initialize(CharacterFacade characterFacade)
    {
        _facade = characterFacade;
        _facade.CharacterSystems.Add(this);
        _library = _facade.Library;
        _agent = _facade.movement.navMeshAgentMovement;
        _weapon = _facade.rangedWeaponController;
        
        _library.GetControlledCharacter(out _player);
        _library.ControlledCharacterChanged += OnControlledCharacterChanged;
    }

    private void OnControlledCharacterChanged(CharacterFacade player)
    {
        if (player is not null) _playerSet = true;
        _player = player;
        
    }


    private void Update()
    {
        if (_playerSet) return;
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, _player.Alignment.FactionLayerMask);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, _player.Alignment.FactionLayerMask);
        
        
        if(!playerInSightRange && !playerInAttackRange) Patrolling();
        if(playerInSightRange && !playerInAttackRange) ChasePlayer();
        if(playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    private void Patrolling()
    {
        if (!isDestinationSet) Search();
        if(isDestinationSet) _agent.SetDestination(destinationPoint);

        Vector3 distanceToDestination = transform.position - destinationPoint;
        if (distanceToDestination.magnitude < 1f) isDestinationSet = false;
    }

    private void Search()
    {
        float positionX = Random.Range(-wanderRange, wanderRange);
        float positionZ = Random.Range(-wanderRange, wanderRange);

        destinationPoint = new Vector3(transform.position.x + positionX, transform.position.y,
            transform.position.z + positionZ);
        if (Physics.Raycast(destinationPoint, -transform.up, 2f, groundLayerMask))
        {
            isDestinationSet = true;
        }
    }

  

    private void ChasePlayer()
    {
        _agent.SetDestination(_player.transform.position);
    }


    private void AttackPlayer()
    {
        _agent.SetDestination(transform.position);   
        transform.LookAt(_facade.transform);

        if (!attacked)
        {
            _weapon.FireWeapon(transform.forward);
            attacked = true;
            
            //TODO implement time dependent timer;
            Invoke(nameof(Reset), timeBetweenAttacks);
        }
    }

    public void Reset()
    {
        attacked = false;
    }

    
    private void OnDestroy()
    {
        _library.ControlledCharacterChanged -= OnControlledCharacterChanged;
    }

    public void Disable()
    {
        enabled = false;
    }
}
