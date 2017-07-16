using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SolarSystem : MonoBehaviour {

    public static SolarSystem SolarSystemInstance;

    public Button galaxyViewButton;
    public Button buildShipButton;

    public Vector3 starPosition { get; set; }

    public GameObject OrbitSpritePrefab;

    FleetManager fleetManager;

    public float solarSystemZoom = 100;
    float storeMaxZoom = 0;

    void OnEnable()
    {
        SolarSystemInstance = this;
        galaxyViewButton.interactable = false;
        buildShipButton.interactable = false;

        fleetManager = GameObject.Find("Fleets").GetComponent<FleetManager>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(mouseRay, out hit))
        {
            Galaxy.GalaxyInstance.MoveSelectionIcon(hit);

            if (Input.GetMouseButtonDown(0) && Galaxy.GalaxyInstance.galaxyView == true)
            {
                Star star = Galaxy.GalaxyInstance.ReturnStarFromGameObject(hit.transform.gameObject);
                starPosition = hit.transform.position;
                Debug.Log("This Star is Called: " + star.starName + "\n" + "It Has " + star.numberOfPlanets + " Planets");

                Galaxy.GalaxyInstance.DestroyGalaxy();
                CreateSolarSystem(star);
            }
        }
        else
        {
            Galaxy.GalaxyInstance.selectionIcon.SetActive(false);
        }

    }

    // This method creates the solar system view after a star is clicked on in the galaxy view
    public void CreateSolarSystem(Star star)
    {
        CameraController.cameraController.ResetCamera();

        Galaxy.GalaxyInstance.selectionIcon.SetActive(false);

        Random.InitState(Galaxy.GalaxyInstance.seedNumber);

        Galaxy.GalaxyInstance.galaxyView = false;

        SpaceObjects.CreateSphereObject(star.starName, Vector3.zero, this.transform);

        for (int i = 0; i < star.planetList.Count; i++)
        {
            Planet planet = star.planetList[i];

            Vector3 planetPos = PositionMath.PlanetPosition(i);

            SpaceObjects.CreateSphereObject(planet.planetName, planetPos, this.transform);

            GameObject orbit = SpaceObjects.CreateOrbitPath(OrbitSpritePrefab, planet.planetName + " Orbit", i + 1, this.transform);
        }

        galaxyViewButton.interactable = true;
        buildShipButton.interactable = true;

        storeMaxZoom = CameraController.cameraController.maxZoom;
        CameraController.cameraController.maxZoom = solarSystemZoom;

        fleetManager.EnableFleets();

    }

    public void DestroySolarSystem()
    {
        while (transform.childCount > 0)
        {
            Transform go = transform.GetChild(0);
            go.SetParent(null);
            Destroy(go.gameObject);
        }

        CameraController.cameraController.MoveTo(starPosition);
        galaxyViewButton.interactable = false;
        buildShipButton.interactable = false;

        CameraController.cameraController.maxZoom = storeMaxZoom;

        GUIManagementScript.GUIManagerInstance.namePlates.Clear();

    }

    /*
   Copyright Shadowplay Coding 2017 - see www.shadowplaycoding.com for licensing details
   Removing this comment forfits any rights given to the user under licensing.
   */
}
