using UnityEngine;

public class WalkState : BaseState
{
    public WalkState(ControlTankWarrior controller) : base(controller) { }

    public override void Enter()
    {
        controller.animator.SetBool("andando", true);
        controller.animator.SetBool("parado", false);
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

        // Check for movement changes
        float vertical = Input.GetAxis("Vertical");
        bool isRunning = controller.playerController.isGrounded && Input.GetKey(KeyCode.LeftShift) && vertical > 0f;

        if (vertical == 0f)
        {
            controller.stateMachine.ChangeState(new IdleState(controller));
        }
        else if (isRunning)
        {
            controller.stateMachine.ChangeState(new RunState(controller));
        }
    }

    public override void Update()
    {
        HandleRotation();
        
        if (controller.playerController.isGrounded)
        {
            float vertical = Input.GetAxis("Vertical");
            controller.moveDirection = controller.transform.forward * vertical * controller.walkSpeed;
        }
        
        ApplyGravity();
        ApplyMovement();
    }

    public override void Exit()
    {
        controller.animator.SetBool("andando", false);
    }
}
