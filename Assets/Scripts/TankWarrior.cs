using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankWarrior : MonoBehaviour
{

	public float runSpeed = 6.5f;
	public float jumpSpeed = 50.0f;
	public float walkSpeed = 1.5f;
	public float rotationSpeed = 60.0f;
	public float gravity = 20;

	public Vector3 walking = Vector3.zero;
	public Vector3 running = Vector3.zero;
	public Vector3 jumping = Vector3.zero;

	public CharacterController control = null;
	public Animator animator = null;

	public bool isJumping = false;
	public bool isRunning = false;

	//Update is called once per frame
	void Update()
	{
		print("is running = " + isRunning);

		isRunning = false;	

		//Moving foward
		if (control.isGrounded) 
		{
			print("is grounded do GET AXIS   " + DateTime.Now);
			walking = new Vector3(0, 0, Input.GetAxis("Vertical"));
			walking = transform.TransformDirection(walking); // Transform Direction transforma as coordenadas x,y e z em coordenadas em relação ao mundo
			walking *= walkSpeed;
		}

		walking.y -= gravity * Time.deltaTime;
		control.Move(walking * Time.deltaTime);
		

		//Rotate Char
		float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
		rotation *= Time.deltaTime;
		transform.Rotate(0, rotation, 0);



		//Animation 
		if (control.isGrounded && Input.GetAxis("Vertical")!= 0)//está no chão e andando
		{
			print("is grounded AND WALKING! ••• " + DateTime.Now + "Walking to: " + Input.GetAxis("Vertical"));

			animator.SetBool("parado", false);
			animator.SetBool("andando", true);
		
		}
		else if (control.isGrounded && Input.GetAxis("Vertical") == 0) //está no chão e parado
		{
			print("is grounded AND IDLE!  ••••••••••• " + DateTime.Now + "IDLE because Input is: " + Input.GetAxis("Vertical"));

			animator.SetBool("parado", true);
			animator.SetBool("andando", false);
		}
		else if (!control.isGrounded)  // está no Ar
		{
			print("is NOT GROUNDED! •••••••••••••••••••••••••••••••• " + DateTime.Now);
			
			animator.SetBool("parado", true);
			animator.SetBool("andando", false);
		}


		////Jump
		//if(control.isGrounded && Input.GetAxis("Jump") != 0)
		//{
		//	isJumping = true;
		//	print("JUMP is " + Input.GetAxis("Jump"));
		//	jumping = new Vector3(0, Input.GetAxis("Jump"), 0);
		//	jumping = transform.TransformDirection(jumping);
		//	jumping *= jumpSpeed;

		//	control.Move(jumping * Time.deltaTime);
		//	isJumping = false;
		//}


		//Run
		if (control.isGrounded && Input.GetKey(KeyCode.LeftShift))
		{
			isRunning = true;

			print("is grounded do RUUUUUNNIIING - CORRE NEGADA   " + DateTime.Now);
			running = new Vector3(0, 0, Input.GetAxis("Vertical"));
			running = transform.TransformDirection(running); // Transform Direction transforma as coordenadas x,y e z em coordenadas em relação ao mundo
			running *= runSpeed;
			control.Move(running * Time.deltaTime);
		}


	}


	private void LateUpdate()
		{
			// colocar a camera aqui com position = posicao da camera em relação ao player
		}
	}
