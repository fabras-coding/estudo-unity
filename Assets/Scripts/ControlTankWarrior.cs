using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTankWarrior : MonoBehaviour
{

	public float runSpeed = 6.5f;
	public float jumpForce = 3.0f;
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
	public bool isAttacking = false;

	//Update is called once per frame
	void Update()
	{


		animator.speed = 1;

		//Moving foward/back
		if (control.isGrounded)
		{
			walking = new Vector3(0, 0, Input.GetAxis("Vertical"));
			walking = transform.TransformDirection(walking); // Transform Direction transforma as coordenadas x,y e z em coordenadas em rela��o ao mundo
			walking *= walkSpeed;
		}

				
		walking.y -= gravity * Time.deltaTime;
		control.Move(walking * Time.deltaTime);

		//Jump 
		if (control.isGrounded && Input.GetButtonDown("Jump"))
		{

			isJumping = true;
			animator.SetBool("pulando", true);
			
			if (isRunning)
				walking.z *= runSpeed;
			
			walking.y = Mathf.Sqrt(jumpForce * gravity);
			

			control.Move(walking * Time.deltaTime);	
		}



		//Rotate Char
		float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
		rotation *= Time.deltaTime;
		transform.Rotate(0, rotation, 0);



		//Animation 
		if (control.isGrounded)
		{


			if (Input.GetAxis("Vertical") != 0)//esta no chao e andando
			{
				
				animator.SetBool("parado", false);
				animator.SetBool("andando", true);

				if (Input.GetButtonDown("Fire1") && !isRunning ) //Atacando
				{
					animator.SetBool("atacando", true);

				}


			}
			else if (Input.GetAxis("Vertical") == 0) //esta no chao e parado
			{
				print("is grounded AND IDLE!  |||||||||||||||| " + DateTime.Now + "IDLE because Input is: " + Input.GetAxis("Vertical"));


				if (Input.GetButtonDown("Fire1")) // Ataque
				{
					print("atacando");
					animator.SetBool("atacando", true);

				}
				else
				{
					animator.SetBool("parado", true);
					animator.SetBool("atacando", false);
					animator.SetBool("andando", false);
				}



			}


		}

		if (!control.isGrounded)  // esta no Ar
		{
			print("is NOT GROUNDED! ||||||||||||||||||||||||||||||||||||| " + DateTime.Now);

			//animator.SetBool("parado", true);
			animator.SetBool("andando", false);
			animator.SetBool("atacando", false);

		}


		//Run
		if (control.isGrounded && Input.GetKey(KeyCode.LeftShift))
		{
			isRunning = true;

			running = new Vector3(0, 0, Input.GetAxis("Vertical"));
			running = transform.TransformDirection(running); // Transform Direction transforma as coordenadas x,y e z em coordenadas em rela��o ao mundo
			running *= runSpeed;
			control.Move(running * Time.deltaTime);
			animator.speed = 2;

		}
		else
			isRunning = false;



		


	}

	public void animationHasEndend(string animationState)
	{

		if (animationState.Equals("Attack"))
		{
			animator.SetBool("atacando", false);
		}

		if (animationState.Equals("Jump"))
		{
			animator.SetBool("pulando", false);
		}

	}


}
