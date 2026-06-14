using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagAreaTrigger : MonoBehaviour
{
	// Start is called before the first frame update
	private void OnTriggerEnter(Collider collider)
	{

		if (collider.gameObject.CompareTag("Player"))
		{
			print("Alguém está na área da Bandeira!");
		}

	}

	private void OnTriggerExit(Collider collider)
	{

		if (collider.gameObject.CompareTag("Player"))
		{
			print("Alguém saiu da área da bandeira!");
		}
	}
}
