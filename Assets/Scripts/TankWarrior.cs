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
	public CharacterController characterController = null;
	public Animator animator = null;

	//Update is called once per frame
	void Update()
	{

		//Moving foward

		//walking.y -= gravity * Time.deltaTime;
		//if (characterController.isGrounded) // nao funciona, ver o pq.

		print("Toquei o chao");
		walking = new Vector3(0, 0, Input.GetAxis("Vertical"));
		walking = transform.TransformDirection(walking); // Transform Direction movimenta o personagem em relação ao mundo
		walking *= walkSpeed;


		print("eixo Y agora: " + walking.y);
		characterController.Move(walking * Time.deltaTime);

		//Rotate Char

		float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
		rotation *= Time.deltaTime;
		print("rotation: " + rotation);
		transform.Rotate(0, rotation, 0);


		//Animation 
		//if (characterController.isGrounded)// n funciona

		if (walking.z != 0)
		{
			print("Valor do walking Z: " + walking.z);
			animator.SetBool("parado", false);
			animator.SetBool("andando", true);
		}
		else
		{
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
