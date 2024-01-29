using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleMovement : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		//realiza a movimentação do objeto
		//o parametro Space é em relação ao que o objeto ira se movimentar, e no caso de Self, é em relação a ele mesmo
		
		transform.Translate(0.01f, 0, 0, Space.Self);

		//Pra não ficar passando zero toda hora nos parametros, podemos usar o Vector3


		//rotação com Self , se fosse World é em relação ao mundo~, não ao objeto
		transform.Rotate(Vector3.right * 0.01f , Space.Self);
		//transform.Rotate(0.01f, 0, 0, Space.Self);

		//scales object
		//transform.localScale += new Vector3(0, 0.05f ,0);

		//transform.localScale += Vector3.up * 0.05f;



	}

	private void FixedUpdate()
	{
		
		
			
		
	}
}
