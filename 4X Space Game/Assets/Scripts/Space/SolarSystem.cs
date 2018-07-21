using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public Material starOwnedMaterial;
    public Material planetColonisedMaterial;

    Dictionary<Planet, GameObject> planetToGameObjectMap;

	public Planet currentPlanet { get; protected set;}

    void OnEnable()
    {
        SolarSystemInstance = this;
        galaxyViewButton.interactable = false;
        buildShipButton.interactable = false;
    }

	void Awake()
	{
		currentPlanet = null;
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
                //Debug.Log("This Star is Called: " + star.starName + "\n" + "It Has " + star.numberOfPlanets + " Planets");

                Galaxy.GalaxyInstance.DestroyGalaxy();
                CreateSolarSystem(star);
            }

            if (Input.GetMouseButtonDown(0) && Galaxy.GalaxyInstance.galaxyView == false)
            {
                Planet planet = ReturnPlanetFromGameObject(hit.transform.gameObject);

                if (planet != null)
                {
                    Debug.Log(planet.planetName + " " + planet.planetType);
					Debug.Log("Credits: " + planet.planetResources.credits + " Minerals: " + planet.planetResources.minerals + " Food: " + planet.planetResources.food);
                    GUIManagementScript.GUIManagerInstance.planetPanel.SetActive(true);
					currentPlanet = planet;

                    if (planet.starBase != null)
                    {
                        GUIManagementScript.GUIManagerInstance.starBasePanel.SetActive(true);
                    }
                }
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
        planetToGameObjectMap = new Dictionary<Planet, GameObject>();

        CameraController.cameraController.ResetCamera();

        Galaxy.GalaxyInstance.selectionIcon.SetActive(false);

        Random.InitState(Galaxy.GalaxyInstance.seedNumber);

        Galaxy.GalaxyInstance.galaxyView = false;

        GameObject starGO = SpaceObjects.CreateSphereObject(star.starName, Vector3.zero, this.transform);

        if (star.starOwned == true)
        {
            starGO.GetComponent<MeshRenderer>().material = starOwnedMaterial;
        }

        for (int i = 0; i < star.planetList.Count; i++)
        {
            Planet planet = star.planetList[i];

            Vector3 planetPos = PositionMath.PlanetPosition(i);

            GameObject planetGO = SpaceObjects.CreateSphereObject(planet.planetName, planetPos, this.transform);

            if (planet.planetColonised == true)
            {
                planetGO.GetComponent<MeshRenderer>().material = planetColonisedMaterial;
            }

            GameObject orbit = SpaceObjects.CreateOrbitPath(OrbitSpritePrefab, planet.planetName + " Orbit", i + 1, this.transform);

            if(planet.planetColonised == true && planet.starBase == null)
            {
                BuildStarBase(planet, planetGO);
            }
            else if(planet.planetColonised == true && planet.starBase != null)
            {
                CreateStarBase(planet, planetGO);
            }

            planetToGameObjectMap.Add(planet, planetGO);
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
        GUIManagementScript.GUIManagerInstance.planetPanel.SetActive(false);
        GUIManagementScript.GUIManagerInstance.starBasePanel.SetActive(false);

    }

    public void BuildStarBase(Planet planetData, GameObject planetGO)
    {
        planetData.starBase = new StarBase();

        CreateStarBase(planetData, planetGO);
    }

    public void CreateStarBase(Planet planetData, GameObject planetGO)
    {
        GameObject starBaseGO = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        starBaseGO.name = planetData.planetName + " Starbase";
        starBaseGO.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        starBaseGO.transform.SetParent(planetGO.transform);
        starBaseGO.transform.localPosition = new Vector3(0.6f, 0.6f, 0.6f);
    }

    public Planet ReturnPlanetFromGameObject(GameObject go)
    {
        if (planetToGameObjectMap.ContainsValue(go))
        {
            int index = planetToGameObjectMap.Values.ToList().IndexOf(go);
            Planet planet = planetToGameObjectMap.Keys.ToList()[index];

            return planet;
        }
        else
        {
            return null;
        }
    }

    /*
   Copyright Shadowplay Coding 2017 - see www.shadowplaycoding.com for licensing details
   Removing this comment forfits any rights given to the user under licensing.
   */
}
