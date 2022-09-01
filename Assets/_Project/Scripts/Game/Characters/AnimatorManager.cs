using System;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator;

    private static readonly int Movement = Animator.StringToHash("movement");
    private static readonly int IsMoving = Animator.StringToHash("isMoving");

    public Vector3 DeltaPosition => animator.deltaPosition;
    public Vector3 RootPosition => animator.rootPosition;
    
    public void Move(float magnitude, float delta)
    {
        animator.SetFloat(Movement, magnitude, 0.05f, delta);
    }

    public void SetMoving(bool isMoving)
    {
        animator.SetBool(IsMoving,isMoving);
    }

    public float Attack()
    {
        // animator.Trigger(attack)
        //return time of animation;
        return 0;
    }
    
}