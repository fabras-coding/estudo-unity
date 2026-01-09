using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPorta1 : MonoBehaviour
{

	public Animator animatorPorta;

	public void OnTriggerEnter(Collider collider)
	{	

		if (collider.gameObject.CompareTag("Personagem"))
		{
			animatorPorta.Play("PortaAbrindo");
			print("porta abriu");
		}
	}

	public void OnTriggerExit(Collider collider) {
		if (collider.gameObject.CompareTag("Personagem"))
		{
			animatorPorta.Play("PortaFechando");
			print("porta fechando");
		}
	}
}
