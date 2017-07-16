using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float panSpeed = 100;

    Transform rotationObject;
    Transform zoomObject;

    // Used before Start()
    void Awake()
    {
        rotationObject = transform.GetChild(0);
        zoomObject = rotationObject.transform.GetChild(0);
        ResetCamera();
    }

    // Update is called once per frame
    void Update() {

        ChangePosition();

    }

    // This method pans the camera view in the XZ plane
    void ChangePosition()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {

            float distance = panSpeed * Time.deltaTime;
            Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

            float dampingFactor = Mathf.Max(Mathf.Abs(Input.GetAxis("Horizontal")), Mathf.Abs(Input.GetAxis("Vertical")));

            transform.Translate(distance * dampingFactor * direction);

        }
    }

    // This method resets the camera to the centre of the scene
    public void ResetCamera()
    {
        this.transform.position = new Vector3(0, 0, 0);
    }


    /*
    Copyright Shadowplay Coding 2017 - see www.shadowplaycoding.com for licensing details
    Removing this comment forfits any rights given to the user under licensing.
    */

}
