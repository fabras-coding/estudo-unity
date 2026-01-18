using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{

	public CharacterController _playerController;
	public Animator _animator;
	private Vector3 _moveDirection;


	[Header("Movimentação")]
	[SerializeField] private float rotationSpeed = 60.0f;
	[SerializeField] private float walkSpeed = 1f;
	[SerializeField] private float runSpeed = 4.5f;
	[SerializeField] private float jumpForce = 3.0f;
	[SerializeField] private float gravity = 20.0f;

	private float vertical;
	private float horizontal;
	private bool isRunning;


	// Start is called before the first frame update
	void Start()
	{

	}

	private void Awake()
	{
		_playerController = GetComponent<CharacterController>();
		_animator = GetComponent<Animator>();
	}

	// Update is called once per frame

	void Update()
	{
		ReadInput();
		HandleRotation();
		HandleAnimator();
		HandleActions();
		ApplyMovement();
	}

	

	void ReadInput()
	{
		vertical = Input.GetAxis("Vertical");
		horizontal = Input.GetAxis("Horizontal");
		isRunning = Input.GetKey(KeyCode.LeftShift);
	}

	void HandleRotation()
	{
		transform.Rotate(0f, horizontal * rotationSpeed * Time.deltaTime, 0f);
	}

	void HandleAnimator()
	{
		float animSpeed = 0f;

		if (vertical != 0f)
			animSpeed = isRunning ? 8f : 1f;

		_animator.SetFloat("speed", animSpeed);
		_animator.SetBool("isGrounded", _playerController.isGrounded);
	}

	void HandleActions()
	{
		if (Input.GetButtonDown("Fire1"))
			_animator.SetTrigger("attack");

		if (Input.GetButtonDown("Jump") && _playerController.isGrounded)
			_animator.SetTrigger("jump");
	}

	void ApplyMovement()
	{
		if (_playerController.isGrounded)
		{
			float moveSpeed = isRunning ? runSpeed : walkSpeed;
			_moveDirection = transform.forward * vertical * moveSpeed;

			if (Input.GetButtonDown("Jump"))
				_moveDirection.y = Mathf.Sqrt(jumpForce * gravity);
		}

		_moveDirection.y -= gravity * Time.deltaTime;
		_playerController.Move(_moveDirection * Time.deltaTime);
	}

}

