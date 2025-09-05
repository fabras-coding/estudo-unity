using UnityEngine;

public abstract class BaseState : IState
{
    protected ControlTankWarrior controller;

    public BaseState(ControlTankWarrior controller)
    {
        this.controller = controller;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
    public virtual void HandleInput() { }

    protected void ApplyGravity()
    {
        controller.moveDirection.y -= controller.gravity * Time.deltaTime;
    }

    protected void ApplyMovement()
    {
        controller.playerController.Move(controller.moveDirection * Time.deltaTime);
    }

    protected void HandleRotation()
    {
        float horizontal = Input.GetAxis("Horizontal");
        controller.transform.Rotate(0f, horizontal * controller.rotationSpeed * Time.deltaTime, 0f);
    }

    protected bool CheckTransitionToAttack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            controller.stateMachine.ChangeState(new AttackState(controller));
            return true;
        }
        return false;
    }

    protected bool CheckTransitionToJump()
    {
        if (controller.playerController.isGrounded && Input.GetButtonDown("Jump"))
        {
            controller.stateMachine.ChangeState(new JumpState(controller));
            return true;
        }
        return false;
    }

    protected bool CheckTransitionToDash()
    {
        if (controller.playerController.isGrounded && Input.GetKeyDown(KeyCode.LeftControl))
        {
            controller.stateMachine.ChangeState(new DashState(controller));
            return true;
        }
        return false;
    }
}
