using UnityEngine;

public class DashState : BaseState
{
    private float dashDuration = 0.3f;
    private float dashSpeed = 20f;
    private float dashTimer;
    private Vector3 dashDirection;

    public DashState(ControlTankWarrior controller) : base(controller) { }

    public override void Enter()
    {
        dashTimer = dashDuration;
        
        // Dash na direção atual do movimento ou para frente se parado
        float vertical = Input.GetAxis("Vertical");
        if (vertical != 0f)
        {
            dashDirection = controller.transform.forward * Mathf.Sign(vertical);
        }
        else
        {
            dashDirection = controller.transform.forward;
        }
        
        controller.moveDirection = dashDirection * dashSpeed;
        
        // Configurar animação (se houver)
        controller.animator.SetBool("dashing", true);
        controller.animator.SetBool("parado", false);
        controller.animator.SetBool("andando", false);
        controller.animator.SetBool("atacando", false);
        
        Debug.Log("Dash iniciado!");
    }

    public override void HandleInput()
    {
        // Durante o dash, só permite ataque como interrupção
        if (CheckTransitionToAttack()) return;
        
        // Rotação limitada durante dash
        float horizontal = Input.GetAxis("Horizontal");
        controller.transform.Rotate(0f, horizontal * controller.rotationSpeed * 0.3f * Time.deltaTime, 0f);
    }

    public override void Update()
    {
        dashTimer -= Time.deltaTime;
        
        // Reduz a velocidade do dash ao longo do tempo
        float dashProgress = 1f - (dashTimer / dashDuration);
        float currentSpeed = Mathf.Lerp(dashSpeed, 0f, dashProgress);
        controller.moveDirection = dashDirection * currentSpeed;
        
        // Finaliza o dash
        if (dashTimer <= 0f)
        {
            // Transição baseada no input atual
            float vertical = Input.GetAxis("Vertical");
            bool isRunning = Input.GetKey(KeyCode.LeftShift) && vertical > 0f;
            
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
        
        ApplyGravity();
        ApplyMovement();
    }

    public override void Exit()
    {
        controller.animator.SetBool("dashing", false);
        Debug.Log("Dash finalizado!");
    }
}
