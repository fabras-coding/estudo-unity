using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
 
	public int maxHealth = 100;
	public int currentHealth { get; private set; }

	public event Action<int, int> OnHealthChanged; // currentHealth, maxHealth
	public event Action OnDeath;
	public event Action<int> OnDamageTaken; // damage amount

	private bool isDead = false;

	private void Awake()
	{
		currentHealth = maxHealth;
	}

	public void TakeDamage(int amount)
	{
		if (isDead || amount <= 0) return;

		currentHealth -= amount;
		currentHealth = Mathf.Max(0, currentHealth);

		OnDamageTaken?.Invoke(amount);
		OnHealthChanged?.Invoke(currentHealth, maxHealth);

		if(currentHealth == 0)
		{
			isDead = true;
			OnDeath?.Invoke();
		}


	}

	public void Heal (int amount)
	{
		if (isDead || amount <= 0) return;
		currentHealth += amount;
		currentHealth = Mathf.Min(maxHealth,currentHealth + amount);
		OnHealthChanged?.Invoke(currentHealth, maxHealth);
	}

}
