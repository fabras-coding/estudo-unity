using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class DeliveryFlagArea : MonoBehaviour
{


	AudioSource audioSource;
	public AudioClip dropFlagAudioClip;
	float timeResetFlag = 5;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}



	private void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.CompareTag("Personagem"))
		{
			print("Você está na área de entrega da bandeira.");

			//There's a flag attached to char
			var flag = GetAllChildren(collision.gameObject).First(x => x.tag == "Flag");
			if (flag != null)
			{

				audioSource.Play();
				print("Parabéns! Você pontuou!");

				print("A BANDEIRA VOLTOU AO LOCAL INICIAL.");

				flag.transform.SetParent(null, true);
				flag.transform.position = new Vector3(-33.017f, 0.9f, -38.792f); //TODO: ARRUMAR ESSA GAMBIARRA (RECEBER DA OUTRA CLASSE)
				audioSource.PlayOneShot(dropFlagAudioClip);
			}

		}
	}


	private void OnTriggerExit(Collider collision)
	{
		if (collision.gameObject.CompareTag("Personagem"))
		{

		}
	}

	private List<GameObject> GetAllChildren(GameObject obj)
	{
		List<GameObject> children = new List<GameObject>();

		foreach (Transform child in obj.transform)
		{
			children.Add(child.gameObject);
			children.AddRange(GetAllChildren(child.gameObject));
		}

		return children;
	}
}
