using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool aim;
		public bool attack;
		public bool MeleeAttack;

        [Header("Vehicle Input Values")]
        public float throttle;  
        public float steer;           
        public bool brake;               
        public bool carExit;            

        [Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
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
        public void OnAim(InputValue value)
        {
            AimInput(value.isPressed);
        }
        public void OnAttack(InputValue value)
        {
            AttackInput(value.isPressed);
        }

        public void OnMeleeAttack(InputValue value)
        {
            MeleeAttackInput(value.isPressed);
        }


        public void OnThrottle(InputValue value)
        {
            throttle = value.Get<float>();
        }

        public void OnSteer(InputValue value)
        {
            steer = value.Get<float>();
        }

        public void OnBrake(InputValue value)
        {
            brake = value.isPressed;
        }

        public void OnCarExit(InputValue value)
        {
            carExit = value.isPressed;
        }


#endif



        public void MoveInput(Vector2 newMoveDirection)
		{
			Debug.Log("Moving");
			move = newMoveDirection;
		} 


		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

        public void AimInput(bool newAimState)
        {
            aim = newAimState;
        }
        public void AttackInput(bool newAttackState)
        {
            attack = newAttackState;
        }

        public void MeleeAttackInput(bool newMeleeAttackState)
        {
            MeleeAttack = newMeleeAttackState;
        }

        private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}