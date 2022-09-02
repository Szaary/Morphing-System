// LocomotionSimpleAgent.cs
// https://docs.unity3d.com/Manual/nav-CouplingAnimationAndNavigation.html
using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshAgentMovement : MonoBehaviour
{
    private AnimatorManager animatorManager;
    private NavMeshAgent agent;
    public Transform head;

    
    private Vector2 smoothDeltaPosition = Vector2.zero;
    private Vector2 velocity = Vector2.zero;
    
    public Vector3 lookAtTargetPosition;
    public float lookAtCoolTime = 0.2f;
    public float lookAtHeatTime = 0.2f;
    public bool looking = true;

    private Vector3 lookAtPosition;
    private Animator animator;
    private float lookAtWeight = 0.0f;
    private CharacterFacade _facade;
    
    public void Initialize(CharacterFacade facade)
    {
        animatorManager = facade.animatorManager;
        _facade = facade;
    }
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        agent.updatePosition = false;
        lookAtTargetPosition = head.position + transform.forward;
        lookAtPosition = lookAtTargetPosition;
    }

    void Update()
    {
        var delta = _facade.timeManager.GetDeltaTime(this);
        var worldDeltaPosition = agent.nextPosition - transform.position;

        // Map 'worldDeltaPosition' to local space
        var dx = Vector3.Dot(transform.right, worldDeltaPosition);
        var dy = Vector3.Dot(transform.forward, worldDeltaPosition);
        var deltaPosition = new Vector2(dx, dy);

        // Low-pass filter the deltaMove
        var smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
        smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);

        // Update velocity if time advances
        if (Time.deltaTime > 1e-5f)
            velocity = smoothDeltaPosition / Time.deltaTime;

        var shouldMove = velocity.magnitude > 0.5f && agent.remainingDistance > agent.radius;

        // Update animation parameters
        animatorManager.SetMoving(shouldMove);

        var magnitude = velocity.magnitude;
        animatorManager.Move(magnitude, delta);

        lookAtTargetPosition = agent.steeringTarget + transform.forward;
        
        if (worldDeltaPosition.magnitude > agent.radius)
            agent.nextPosition = transform.position + 0.9f*worldDeltaPosition;
    }

    private void OnAnimatorIK(int layerIndex)
    {
        lookAtTargetPosition.y = head.position.y;
        var lookAtTargetWeight = looking ? 1.0f : 0.0f;

        var curDir = lookAtPosition - head.position;
        var futDir = lookAtTargetPosition - head.position;

        curDir = Vector3.RotateTowards(curDir, futDir, 6.28f*Time.deltaTime, float.PositiveInfinity);
        lookAtPosition = head.position + curDir;

        var blendTime = lookAtTargetWeight > lookAtWeight ? lookAtHeatTime : lookAtCoolTime;
        lookAtWeight = Mathf.MoveTowards (lookAtWeight, lookAtTargetWeight, Time.deltaTime/blendTime);
        animator.SetLookAtWeight (lookAtWeight, 0.2f, 0.5f, 0.7f, 0.5f);
        animator.SetLookAtPosition (lookAtPosition);
    }

    void OnAnimatorMove ()
    {
        var position = animatorManager.RootPosition;
        position.y = agent.nextPosition.y;
        transform.position = position;
    }

    public void SetDestination(Vector3 position)
    {
        agent.destination = position;
    }

    private void OnEnable()
    {
        agent ??= GetComponent<NavMeshAgent>();
        agent.enabled = true;
    }

    private void OnDisable()
    {
        agent.enabled = false;
    }
}