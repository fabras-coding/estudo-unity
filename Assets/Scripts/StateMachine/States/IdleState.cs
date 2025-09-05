using UnityEngine;

public class IdleState : BaseState
{
    public IdleState(ControlTankWarrior controller) : base(controller) { }

    public override void Enter()
    {
        controller.animator.SetBool("parado", true);
        controller.animator.SetBool("andando", false);
        controller.animator.SetBool("atacando", false);
        controller.animator.speed = 1f;
    }

    public override void HandleInput()
    {
        // Check for attack first (highest priority)
        if (CheckTransitionToAttack()) return;
        
        // Check for dash
        if (CheckTransitionToDash()) return;
        
        // Check for jump
        if (CheckTransitionToJump()) return;

        // Check for movement
        float vertical = Input.GetAxis("Vertical");
        bool isRunning = controller.playerController.isGrounded && Input.GetKey(KeyCode.LeftShift) && vertical > 0f;

        if (vertical != 0f)
        {
            if (isRunning)
            {
                controller.stateMachine.ChangeState(new RunState(controller));
            }
            else
            {
                controller.stateMachine.ChangeState(new WalkState(controller));
            }
        }
    }

    public override void Update()
    {
        HandleRotation();
        
        if (controller.playerController.isGrounded)
        {
            controller.moveDirection = Vector3.zero;
        }
        
        ApplyGravity();
        ApplyMovement();
    }

    public override void Exit()
    {
        controller.animator.SetBool("parado", false);
    }
}
