using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
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
    private static readonly int Fire = Animator.StringToHash("fire");

    //private Dictionary<string, float> animationTimes = new();

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
            // animationTimes.Clear();
            // var clips = animator.runtimeAnimatorController.animationClips;
            // foreach (var clip in clips)
            // {
            //     animationTimes.Add(clip.name, clip.length);
            // }
        }        
    }

    public void Move(float magnitude, float delta)
    {
        animator.SetFloat(Movement, magnitude, 0.05f, delta);
    }

    public void SetMoving(bool isMoving)
    {
        animator.SetBool(IsMoving,isMoving);
    }

    public void Attack()
    {
        animator.SetTrigger(Fire);
        //return animationTimes["attack"];
    }
    
}