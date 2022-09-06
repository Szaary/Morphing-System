using UnityEngine;

public class AnimatorManager : MonoBehaviour, IDoActions
{
    public int ActionPoints { get; private set; }
    
    public enum AnimationsType
    {
        Universal,
        Riffle
    }

    private AnimationsType _currentType;

    public Animator animator;
    [SerializeField] private AnimatorOverrideController riffleAnimator;


    private static readonly int Movement = Animator.StringToHash("movement");
    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    private static readonly int RangedAttack = Animator.StringToHash("rangedStrongAttack");
    private static readonly int Hit = Animator.StringToHash("hit");
    private static readonly int DefensiveSkill = Animator.StringToHash("defensiveSkill");
    private static readonly int Dead = Animator.StringToHash("dead");

    public Vector3 DeltaPosition => animator.deltaPosition;
    public Vector3 RootPosition => animator.rootPosition;

    private void Awake()
    {
        ChangeOverrideAnimator(AnimationsType.Riffle);
    }

    public void ChangeOverrideAnimator(AnimationsType type)
    {
        if (type == _currentType) return;

        if (type == AnimationsType.Riffle)
        {
            _currentType = type;
            animator.runtimeAnimatorController = riffleAnimator;
        }
    }

    public void Move(float magnitude, float delta)
    {
        animator.SetFloat(Movement, magnitude, 0.05f, delta);
    }

    public void SetMoving(bool isMoving)
    {
        animator.SetBool(IsMoving, isMoving);
    }


    public void Attack()
    {
        ActionPoints++;
        animator.SetTrigger(RangedAttack);
    }

    public void Defensive()
    {
        ActionPoints++;
        animator.SetTrigger(DefensiveSkill);
    }

    public void GetHit()
    {
        animator.SetTrigger(Hit);
    }

    public void Death()
    {
        animator.SetTrigger(Dead);
    }

    
}