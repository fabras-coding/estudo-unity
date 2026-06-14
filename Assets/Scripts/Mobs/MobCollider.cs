using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MobCollider : MonoBehaviour
{

	public int damage = 10;
	public float damageCooldown = 1f;
	public string targetTag = "Player";

	private float lastDamageTime = -999f;

	private void Reset()
	{
		Collider c = GetComponent<Collider>();
		if (c) c.isTrigger = true;
	}

	private void OnTriggerEnter(Collider collider)
	{
		if (!collider.CompareTag(targetTag))
			return;
		var health = collider.GetComponent<Health>();
		if (health != null)
		{
			health.TakeDamage(damage);
			lastDamageTime = Time.time;
			Console.WriteLine("/////////////\\\\\\\\\\\\\\\\ Player took " + damage + " damage from mob.");
		}
		else
		{
			// se o player usa outro componente, adapte aqui
			var controller = collider.GetComponent<PlayerController>();
			//if (controller != null) controller.ReceiveDamage(damage); } }
		}
	}

	private void OnTriggerStay(Collider collider)
	{
		if (!collider.CompareTag(targetTag))
			return;

		if (Time.time - lastDamageTime >= damageCooldown)
		{
			var health = collider.GetComponent<Health>();
			if (health != null)
			{
				health.TakeDamage(damage);
				lastDamageTime = Time.time;
				Console.WriteLine("/////////////\\\\\\\\\\\\\\\\ Player took " + damage + " damage from mob.");
			}
			else
			{
				// se o player usa outro componente, adapte aqui
				var controller = collider.GetComponent<PlayerController>();
				//if (controller != null) controller.ReceiveDamage(damage); } }
			}
		}
	}

}
