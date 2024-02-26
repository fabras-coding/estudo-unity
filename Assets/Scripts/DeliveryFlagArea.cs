using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryFlagArea : MonoBehaviour
{

	bool canLetFlag = false;

	private void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.CompareTag("Personagem"))
		{
			print("Voc� est� na �rea de entrega da bandeira.");
			canLetFlag = true;
		}
	}


	private void OnTriggerExit(Collider collision)
	{
		if (collision.gameObject.CompareTag("Personagem"))
		{
			canLetFlag = false;
		}
	}
}
