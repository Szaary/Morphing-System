using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEditor.Animations;
using UnityEngine;

public class AnimatorMovementController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private float rotationSpeed;

    [SerializeField] private float jumpSpeed;

    [SerializeField] private float jumpButtonGracePeriod;

    private Transform cameraTransform;

    private CharacterController characterController;
    private float ySpeed;
    private float originalStepOffset;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    private float _delta;

    private MovementInput _input;
    private CharacterFacade _facade;
    private static readonly int Movement = Animator.StringToHash("movement");
    private static readonly int IsMoving = Animator.StringToHash("isMoving");

    public void Initialize(CharacterFacade characterFacade)
    {
        _facade = characterFacade;
        cameraTransform = characterFacade.cameraManager.MainCamera.transform;
        _input = characterFacade.movementInput;
    }

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
    }

    // Update is called once per frame
    void Update()
    {
        _delta = _facade.timeManager.GetDeltaTime(this);

        float horizontalInput = _input.move.x; // Input.GetAxis("Horizontal");
        float verticalInput = _input.move.y; //Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);


        animator.SetFloat(Movement, inputMagnitude, 0.05f, _delta);


        // if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        // {
        //     inputMagnitude /= 2;
        // }

        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) *
                            movementDirection;
        movementDirection.Normalize();

        ySpeed += Physics.gravity.y * _delta;

        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;
        }

        if (_input.jump)
        {
            jumpButtonPressedTime = Time.time;
        }

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;

            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                ySpeed = jumpSpeed;
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }
        else
        {
            characterController.stepOffset = 0;
        }


        if (movementDirection != Vector3.zero)
        {
            animator.SetBool(IsMoving,true);
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * _delta);
        }
        else
        {
            animator.SetBool(IsMoving,false);
        }
    }

    private void OnAnimatorMove()
    {
        Vector3 velocity = animator.deltaPosition;
        velocity.y = ySpeed * _delta;

        characterController.Move(velocity);
    }
}