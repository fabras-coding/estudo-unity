using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagAreaTrigger : MonoBehaviour
{
	// Start is called before the first frame update
	private void OnTriggerEnter(Collider collider)
	{

		if (collider.gameObject.CompareTag("Personagem"))
		{
			print("Algu�m est� na �rea da Bandeira!");
		}

	}

	private void OnTriggerExit(Collider collider)
	{

		if (collider.gameObject.CompareTag("Personagem"))
		{
			print("Algu�m saiu da �rea da bandeira!");
		}
	}
}
