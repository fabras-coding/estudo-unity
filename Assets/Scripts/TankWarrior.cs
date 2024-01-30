using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankWarrior : MonoBehaviour
{
	/// <summary>
	/// CODIGO DESCOMENTADO é a maneira simples de fazer o personagem de mover.Mas na maneira mais correta (comentada) nao ta funfando KKKK
	/// </summary>

	public float walkSpeed = 5.0f;
	public float rotationSpeed = 50;
	public float gravity = 20;
	public Vector3 walking = Vector3.zero;
	public CharacterController control = null;
	public Animator animator = null;

	//Update is called once per frame
	void Update()
	{

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



		//}


		//public float moveSpeed = 5.0f;


		//void Update() // Funciona kkk
		//{

		//	//Moving foward


		//	float move = Input.GetAxis("Vertical") * moveSpeed;
		//	float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

		//	move *= Time.deltaTime;
		//	rotation *= Time.deltaTime;



		//	//Rotate Char

		//	transform.Rotate(0, rotation, 0);
		//	transform.Translate(0, 0, move);

		//	//Animação
		//	if (Input.GetKey(KeyCode.W)) //gambi kk
		//	{
		//		animator.SetBool("parado", false);
		//		animator.SetBool("andando", true);
		//	}
		//	else
		//	{
		//		animator.SetBool("parado", true);
		//		animator.SetBool("andando", false);
		//	}



	}


		private void LateUpdate()
		{
			// colocar a camera aqui com position = posicao da camera em relação ao player
		}
	}
