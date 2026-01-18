using UnityEngine;

public class NeonPulse : MonoBehaviour
{
	public Color neonColor = Color.magenta;
	public float pulseSpeed = 5f;
	public float intensity = 3f;

	private Material mat;

	void Start()
	{
		mat = GetComponent<Renderer>().material;
	}

	void Update()
	{
		float emission = Mathf.PingPong(Time.time * pulseSpeed, intensity);
		Color finalColor = neonColor * emission;
		mat.SetColor("_EmissionColor", finalColor);
	}
}
