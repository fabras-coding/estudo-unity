using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraAvoidWalls : MonoBehaviour
{
	// Start is called before the first frame update

	public void OnTriggerEnter(Collider collider)
	{
		print("Colidiu trigger");
		//collision.gameObject.CompareTag("");


	}

	public void OnTriggerExit(Collider collider)
	{
		print("saiu trigger");
		//collision.gameObject.CompareTag("");


	}

	public void OnCollisionEnter(Collision collision)
	{
		print("Colidiu");

	}

	public void OnCollisionExit(Collision collision)
	{
		print("saiu collision");

	}
}
