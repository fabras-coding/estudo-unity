using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBasic1 : MonoBehaviour
{
	// Start is called before the first frame update
	private void OnTriggerEnter(Collider collider)
	{

		if (collider.gameObject.CompareTag("Personagem"))
		{
			print("Você está na área 1.");
		}

	}

	private void OnTriggerExit(Collider collider)
	{

		if (collider.gameObject.CompareTag("Personagem"))
		{
			print("Você saiu da área 1.");
		}
	}
}
