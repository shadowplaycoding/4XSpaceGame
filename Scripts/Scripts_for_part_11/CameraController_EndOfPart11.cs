using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public static CameraController cameraController;

    public float panSpeed = 5;
    public float zoomedInAngle = 45;
    public float zoomedOutAngle = 90;
    public float minZoom = 20; // Minimum distance away from XZ plane 0.
    public float maxZoom = 200; // Maximum distance away from XZ plane 0.
    public bool inverseZoom = false;

    public static Quaternion currentAngle;

    float zoomLevel = 0;

    Transform rotationObject;
    Transform zoomObject;

    // Used before Start()
    void Awake()
    {
        cameraController = this;
        rotationObject = transform.GetChild(0);
        zoomObject = rotationObject.transform.GetChild(0);
        ResetCamera();
    }

    // Update is called once per frame
    void Update() {

        ChangeZoom();
        ChangePosition();

    }

    // This method pans the camera view in the XZ plane
    void ChangePosition()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            float movementFactor = Mathf.Lerp(minZoom, maxZoom, zoomLevel);
            float distance = panSpeed * Time.deltaTime;
            Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

            float dampingFactor = Mathf.Max(Mathf.Abs(Input.GetAxis("Horizontal")), Mathf.Abs(Input.GetAxis("Vertical")));

            transform.Translate(distance * dampingFactor * movementFactor * direction);

            ClampCameraPan();

        }
    }

    // This method changes the zoom of the camera
    void ChangeZoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            if (inverseZoom == false)
            {
                zoomLevel = Mathf.Clamp01(zoomLevel - Input.GetAxis("Mouse ScrollWheel"));
            }
            else
            {
                zoomLevel = Mathf.Clamp01(zoomLevel + Input.GetAxis("Mouse ScrollWheel"));
            }

            float zoom = Mathf.Lerp(-minZoom, -maxZoom, zoomLevel);
            zoomObject.transform.localPosition = new Vector3(0, 0, zoom);

            float zoomAngle = Mathf.Lerp(zoomedInAngle, zoomedOutAngle, zoomLevel);
            rotationObject.transform.localRotation = Quaternion.Euler(zoomAngle, 0, 0);
            currentAngle = rotationObject.transform.localRotation;

        }
    }


    // This method resets the camera to the centre of the scene
    public void ResetCamera()
    {
        this.transform.position = new Vector3(0, 0, 0);
        zoomLevel = 0;
        rotationObject.transform.rotation = Quaternion.Euler(zoomedInAngle, 0, 0);
        currentAngle = rotationObject.transform.rotation;
        zoomObject.transform.localPosition = new Vector3(0, 0, -minZoom);
    }

    // This method stops the camera 
    void ClampCameraPan()
    {
        Vector3 position = this.transform.position;

        if (Galaxy.GalaxyInstance.galaxyView == true)
        {
            position.x = Mathf.Clamp(transform.position.x, -Galaxy.GalaxyInstance.maximumRadius, Galaxy.GalaxyInstance.maximumRadius);
            position.z = Mathf.Clamp(transform.position.z, -Galaxy.GalaxyInstance.maximumRadius, Galaxy.GalaxyInstance.maximumRadius);
        }
        else
        {
            position.x = Mathf.Clamp(transform.position.x, -50, 50);
            position.z = Mathf.Clamp(transform.position.z, -50, 50);
        }

        this.transform.position = position;

    }

    public void MoveTo(Vector3 position)
    {
        this.transform.position = position;
    }

    /*
    Copyright Shadowplay Coding 2017 - see www.shadowplaycoding.com for licensing details
    Removing this comment forfits any rights given to the user under licensing.
    */

}
