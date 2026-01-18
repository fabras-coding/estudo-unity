using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{

    public Camera mainCamera;
    public Camera backCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera.enabled = true;
        backCamera.enabled = false;

	}

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(2))
        {
            mainCamera.enabled = false;
            backCamera.enabled = true;
		}

		if (Input.GetMouseButtonUp(2)) 
        {
            mainCamera.enabled = true; 
            backCamera.enabled = false; 
        }

	}
}
