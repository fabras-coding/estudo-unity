using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GetFlagAreaTrigger : MonoBehaviour
{

	bool canTakeFlag = false;
	bool isFlagCarrying = false;
	public GameObject flag;
	public GameObject getFlagArea;
	public float timeToGetFlag = 2;
	public float timeToFlagGoBack = 20;
	public Vector3 flagStartPosition = Vector3.zero;

	private void Start()
	{

		flagStartPosition = flag.transform.position;

	}

	private void Update()
	{

		getFlagArea.transform.parent = flag.transform;
		timeToGetFlag -= Time.deltaTime;

		if(!isFlagCarrying)
			timeToFlagGoBack -= Time.deltaTime;


		if(timeToFlagGoBack <= 0 && !isFlagCarrying)
		{
			print("A BANDEIRA VOLTOU AO LOCAL INICIAL.");
			timeToFlagGoBack = 20;
			flag.transform.position = flagStartPosition;
		}


	}

	private void OnTriggerStay(Collider collider)
	{
		if (canTakeFlag && timeToGetFlag <= 0)
		{
			if (Input.GetKeyDown(KeyCode.E) && collider.gameObject.CompareTag("Personagem"))
			{
				print("=--=-=-=-==--=-=-=-=-=-=-=-=-=-=-=-=-==-=--==--=-=-= Você pegou a bandeira." + DateTime.Now);
				isFlagCarrying = true;
				flag.transform.parent = collider.gameObject.transform;
				canTakeFlag = false;
				timeToFlagGoBack = 20;


			}
		}
		
		else if (isFlagCarrying)
		{
			print("**********Você pode soltar a bandeira a qualquer momento apertando o E.**************" + DateTime.Now);
			if (Input.GetKeyDown(KeyCode.E))
			{
				flag.transform.SetParent(null, true);
				print("**********Você soltou a bandeira**************");
				isFlagCarrying = false;
				canTakeFlag = true;
				timeToGetFlag = 2;

			}

		}



	}

	private void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.CompareTag("Personagem"))
		{
			canTakeFlag = true;
			print("Pressione E para pegar a bandeira!");

		}

	}

	private void OnTriggerExit(Collider collider)
	{

		if (collider.gameObject.CompareTag("Personagem"))
			canTakeFlag = false;

	}
}
