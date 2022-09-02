using StarterAssets;
using UnityEngine;

public class AnimatorMovementController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed=200;

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
    private AnimatorManager _animatorManager;

    public void Initialize(CharacterFacade characterFacade)
    {
        _facade = characterFacade;
        _animatorManager = _facade.animatorManager;
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
        

        if (_input.walk)
        {
            inputMagnitude /= 2;
        }
        if (_input.sprint)
        {
            inputMagnitude *= 2;
        }

        _animatorManager.Move(inputMagnitude, _delta);


        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
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
            
            _animatorManager.SetMoving(true);
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * _delta);
        }
        else
        {
            _animatorManager.SetMoving(false);
        }
    }

    private void OnAnimatorMove()
    {
        Vector3 velocity = _animatorManager.DeltaPosition;
        velocity.y = ySpeed * _delta;

        characterController.Move(velocity);
    }
}