using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MastTrigger : MonoBehaviour
{

    AudioSource audioSource;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}

	// Start is called before the first frame update
	void Start()
    {
        print("STARTOU O SCRIP");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.CompareTag("Personagem"))
        {
            print("Colidiu com a bandeira");
            audioSource.Play();
        }
	}
}
