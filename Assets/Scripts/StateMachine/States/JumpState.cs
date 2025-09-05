using UnityEngine;

public class JumpState : BaseState
{
    public JumpState(ControlTankWarrior controller) : base(controller) { }

    public override void Enter()
    {
        // Apply jump force
        controller.moveDirection.y = Mathf.Sqrt(controller.jumpForce * controller.gravity);
        
        // Keep current animation but could add jump animation here if available
        controller.animator.SetBool("atacando", false);
    }

    public override void HandleInput()
    {
        // Check for attack while in air
        if (CheckTransitionToAttack()) return;

        // Continue movement input while in air
        float vertical = Input.GetAxis("Vertical");
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && vertical > 0f;
        
        if (controller.playerController.isGrounded)
        {
            // Land and transition to appropriate state
            if (vertical == 0f)
            {
                controller.stateMachine.ChangeState(new IdleState(controller));
            }
            else if (isRunning)
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
        
        // Allow some air movement
        if (!controller.playerController.isGrounded)
        {
            float vertical = Input.GetAxis("Vertical");
            float speed = Input.GetKey(KeyCode.LeftShift) ? controller.runSpeed : controller.walkSpeed;
            controller.moveDirection.x = controller.transform.forward.x * vertical * speed * 0.5f; // Reduced air control
            controller.moveDirection.z = controller.transform.forward.z * vertical * speed * 0.5f;
        }
        
        ApplyGravity();
        ApplyMovement();
    }
}
