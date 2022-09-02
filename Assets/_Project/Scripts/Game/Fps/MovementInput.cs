using System;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
    public class MovementInput : MonoBehaviour
    {
        [Header("Character Input Values")] public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;

        public bool shoot;
        public bool melee;
        public bool walk;


        [Header("Movement Settings")] public bool analogMovement;

        [Header("Mouse Cursor Settings")] [HideInInspector]
        public bool cursorInputForLook = true;


#if ENABLE_INPUT_SYSTEM
        public void OnMove(InputValue value)
        {
            MoveInput(value.Get<Vector2>());
        }

        public void OnLook(InputValue value)
        {
            if (cursorInputForLook)
            {
                LookInput(value.Get<Vector2>());
            }
        }

        public void OnJump(InputValue value)
        {
            JumpInput(value.isPressed);
        }

        public void OnSprint(InputValue value)
        {
            SprintInput(value.isPressed);
        }

        public void OnFire(InputValue value)
        {
            ShootInput(value.isPressed);
        }

        public void OnMelee(InputValue value)
        {
            MeleeInput(value.isPressed);
        }

        public void OnWalk(InputValue value)
        {
            WalkInput(value.isPressed);
        }


#endif

        private void MeleeInput(bool valueIsPressed)
        {
            melee = valueIsPressed;
        }

        private void ShootInput(bool newShootState)
        {
            shoot = newShootState;
        }

        private void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;
        }

        private void LookInput(Vector2 newLookDirection)
        {
            look = newLookDirection;
        }

        private void JumpInput(bool newJumpState)
        {
            jump = newJumpState;
        }

        private void SprintInput(bool newSprintState)
        {
            //sprint = newSprintState;
            sprint = !sprint;
        }

        private void WalkInput(bool newWalkState)
        {
            walk = !walk;
        }
    }
}