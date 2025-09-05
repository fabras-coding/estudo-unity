using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class ControlTankWarrior : MonoBehaviour
{
	[Header("Movimentação")]
	[SerializeField] public float walkSpeed = 1.5f;
	[SerializeField] public float runSpeed = 6.5f;
	[SerializeField] public float jumpForce = 3.0f;
	[SerializeField] public float gravity = 20.0f;
	[SerializeField] public float rotationSpeed = 60.0f;

	[HideInInspector] public CharacterController playerController;
	[HideInInspector] public Animator animator;
	[HideInInspector] public Vector3 moveDirection;
	[HideInInspector] public StateMachine stateMachine;

	// Legacy compatibility
	public CharacterController _playerController => playerController;
	public Animator _animator => animator;


	private void Awake()
	{
		animator = GetComponent<Animator>();
		playerController = GetComponent<CharacterController>();
		
		// Initialize state machine
		stateMachine = new StateMachine();
	}

	private void Start()
	{
		// Start with idle state
		stateMachine.Initialize(new IdleState(this));
	}

	// Update is called once per frame
	void Update()
	{
		stateMachine.Update();
	}

	// Animation event callbacks - maintain compatibility
	public void OnAttackEnd()
	{
		if (stateMachine.IsInState<AttackState>())
		{
			stateMachine.ChangeState(new IdleState(this));
		}
	}

	public void OnWalkEnd()
	{
		if (stateMachine.IsInState<WalkState>())
		{
			stateMachine.ChangeState(new IdleState(this));
		}
	}

	// public void animationHasEndend(string animationState)
	// {
	// 	if (animationState.Equals("Attack"))
	// 	{
	// 		if (stateMachine.IsInState<AttackState>())
	// 		{
	// 			stateMachine.ChangeState(new IdleState(this));
	// 		}
	// 	}
	// }
}
