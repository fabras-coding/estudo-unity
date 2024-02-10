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
			print("Voc� est� na �rea Pirata!");
		}

	}

	private void OnTriggerExit(Collider collider)
	{

		if (collider.gameObject.CompareTag("Personagem"))
		{
			print("Voc� saiu da �rea Pirata!");
		}
	}
}
