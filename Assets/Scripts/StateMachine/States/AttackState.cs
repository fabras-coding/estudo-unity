using UnityEngine;

public class AttackState : BaseState
{
    public AttackState(ControlTankWarrior controller) : base(controller) { }

    public override void Enter()
    {
        controller.animator.SetBool("atacando", true);
        controller.animator.SetBool("andando", false);
        controller.animator.SetBool("parado", false);
    }

    public override void HandleInput()
    {
        // During attack, we don't process most inputs
        // The state will be changed by animation events or the OnAttackEnd method
        
        // Still allow rotation during attack
        HandleRotation();
    }

    public override void Update()
    {
        // Stop horizontal movement during attack
        if (controller.playerController.isGrounded)
        {
            controller.moveDirection.x = 0f;
            controller.moveDirection.z = 0f;
        }
        
        ApplyGravity();
        ApplyMovement();
    }

    public override void Exit()
    {
        controller.animator.SetBool("atacando", false);
    }
}
