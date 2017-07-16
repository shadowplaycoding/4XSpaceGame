using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIManagementScript : MonoBehaviour {

    public static GUIManagementScript GUIManagerInstance;

    public List<GameObject> namePlates;

    

    void OnEnable()
    {
        GUIManagerInstance = this;
        namePlates = new List<GameObject>();
    }

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        if(namePlates.Count > 0)
        {
            if (CameraController.cameraController.zoomLevel < 0.4)
            {
                for (int i = 0; i < namePlates.Count; i++)
                {
                    namePlates[i].transform.LookAt(Camera.main.transform);
                    float rotY = namePlates[i].transform.localEulerAngles.y + 180f;
                    namePlates[i].transform.localEulerAngles = new Vector3(
                        -namePlates[i].transform.localEulerAngles.x,
                        rotY,
                        -namePlates[i].transform.localEulerAngles.z);
                }
            }
        }

	}

    /*
    Copyright Shadowplay Coding 2017 - see www.shadowplaycoding.com for licensing details
    Removing this comment forfits any rights given to the user under licensing.
    */
}
