using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeonPulse : MonoBehaviour
{
	public Material neonMat;
	public float pulseSpeed = 2f;
	private Color baseColor;

	void Start()
	{
		baseColor = neonMat.GetColor("_EmissionColor");
	}

	void Update()
	{
		float emission = Mathf.PingPong(Time.time * pulseSpeed, 1f);
		Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);
		neonMat.SetColor("_EmissionColor", finalColor);
	}
}
