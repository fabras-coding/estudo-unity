/*
 * VERSÃO ORIGINAL DO CONTROLTANKWARRIOR (ANTES DA REFATORAÇÃO)
 * 
 * Este arquivo serve como referência para comparar com a versão refatorada.
 * A funcionalidade é idêntica, mas a organização é completamente diferente.
 * 
 * PROBLEMAS DA VERSÃO ORIGINAL:
 * - Tudo em uma classe (Monolítico)
 * - Difícil de adicionar novos estados
 * - Lógica misturada no Update()
 * - Estados hardcoded com enums
 * - Difícil de testar individualmente
 * 
 * VERSÃO REFATORADA:
 * - Cada estado é uma classe separada
 * - Fácil adicionar novos estados
 * - Lógica separada por responsabilidade
 * - Estados são objetos com comportamento
 * - Cada estado pode ser testado individualmente
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class ControlTankWarriorOriginal : MonoBehaviour
{
	//TODO: Refactor ✅ FEITO!
	[Header("Movimentação")]
	[SerializeField] private float walkSpeed = 1.5f;
	[SerializeField] private float runSpeed = 6.5f;
	[SerializeField] private float jumpForce = 3.0f;
	[SerializeField] private float gravity = 20.0f;
	[SerializeField] private float rotationSpeed = 60.0f;

	public CharacterController _playerController;
	public Animator _animator;
	private Vector3 _moveDirection;
	private bool _isRunning;

	private enum State { Idle, Walk, Run, Jump, Attack }
	private State _state = State.Idle;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
		_playerController = GetComponent<CharacterController>();
	}

	//Update is called once per frame
	void Update()
	{
		// PROBLEMA: Tudo misturado em um lugar só
		ProcessInput();     // 40+ linhas de código
		ApplyMovement();    // Simples mas misturado
		UpdateAnimation();  // Estados hardcoded
	}

	private void ProcessInput()
	{
		// PROBLEMA: Método muito grande com muitas responsabilidades
		float vertical = Input.GetAxis("Vertical");
		float horizontal = Input.GetAxis("Horizontal");
		bool jumpPressed = Input.GetButtonDown("Jump");
		bool attackPressed = Input.GetButtonDown("Fire1");

		// Rotation
		transform.Rotate(0f, horizontal * rotationSpeed * Time.deltaTime, 0f);

		//Check if is running
		_isRunning = _playerController.isGrounded && Input.GetKey(KeyCode.LeftShift) && vertical > 0f;

		//On ground movement
		if (_playerController.isGrounded)
		{
			float speed = _isRunning ? runSpeed : walkSpeed;
			_moveDirection = transform.forward * vertical * speed;

			if (jumpPressed)
			{
				_moveDirection.y = Mathf.Sqrt(jumpForce * gravity);
				_state = State.Jump;
				return;
			}
		}

		//Gravity
		_moveDirection.y -= gravity * Time.deltaTime;

		//Movement State
		// PROBLEMA: Lógica de transição misturada com input
		if (_state != State.Jump && _state != State.Attack)
		{
			if (_isRunning) _state = State.Run;
			else if (vertical != 0f) _state = State.Walk;
			else _state = State.Idle;
		}

		//Attack
		if (attackPressed)
			_state = State.Attack;
	}

	private void ApplyMovement()
	{
		_playerController.Move(_moveDirection * Time.deltaTime);
	}

	private void UpdateAnimation()
	{
		// PROBLEMA: Estados hardcoded, difícil de escalar
		_animator.speed = _isRunning ? 2f : 1f;
		_animator.SetBool("andando", _state == State.Walk);
		_animator.SetBool("parado", _state == State.Idle);
		_animator.SetBool("atacando", _state == State.Attack);
	}

	public void OnAttackEnd()
	{
        if (_state == State.Attack)
        {
            _state = State.Idle;
        }
    }

	public void OnWalkEnd()
	{
		if (_state == State.Walk)
		{
			_state = State.Idle;
		}
	}

	public void animationHasEndend(string animationState)
	{
		if (animationState.Equals("Attack"))
		{
			_animator.SetBool("atacando", false);
		}
	}
}

/*
 * COMPARAÇÃO: VERSÃO ORIGINAL VS REFATORADA
 * 
 * ORIGINAL:
 * - 1 arquivo com ~100 linhas
 * - Tudo em uma classe
 * - ProcessInput() com 40+ linhas
 * - Estados como enum simples
 * - Transições hardcoded
 * 
 * REFATORADA:
 * - 9 arquivos organizados
 * - Cada estado é uma classe
 * - Cada método tem responsabilidade única
 * - Estados como objetos com comportamento
 * - Transições flexíveis e escaláveis
 * 
 * RESULTADO:
 * - Mais fácil de entender
 * - Mais fácil de manter
 * - Mais fácil de testar
 * - Mais fácil de expandir
 * - Mais profissional
 */
