using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankWarrior : MonoBehaviour
{
	//CODIGO DESCOMENTADO é a maneira simples de fazer o personagem de mover. Mas na maneira mais correta (comentada) nao ta funfando KKKK

	//public float walkSpeed = 5.0f;
	//public float rotationSpeed = 15.5f;
	//public float gravity = 20;
	//public Vector3 walking = Vector3.zero;
	public CharacterController characterController = null;

	// Update is called once per frame
	//void Update()
	//{

	//	//Moving foward

	//	if (characterController.isGrounded)
	//	{
	//		walking = new Vector3(0, 0, Input.GetAxis("Vertical"));
	//		walking = transform.TransformDirection(walking); // Transform Direction movimenta o personagem em relação ao mundo
	//		walking *= walkSpeed;
	//	}
	//	//walking.y -= gravity * Time.deltaTime;
	//	characterController.Move(walking * Time.deltaTime);

	//	//Rotate Char

	//	float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
	//	rotation *= Time.deltaTime;

	//	transform.Rotate(0, rotation, 0);



	//}


	public float moveSpeed = 5.0f;
	public float rotationSpeed = 50;

	void Update() // Funciona kkk
	{

		//Moving foward


		float move = Input.GetAxis("Vertical") * moveSpeed;
		float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

		move *= Time.deltaTime;
		rotation *= Time.deltaTime;

	    //characterController.Move(walking * Time.deltaTime);

		//Rotate Char

		transform.Rotate(0, rotation, 0);
		transform.Translate(0, 0, move);



	}


	private void LateUpdate()
	{
		// colocar a camera aqui com position = posicao da camera em relação ao player
	}
}
