using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SolarSystem : MonoBehaviour {

    public static SolarSystem SolarSystemInstance;

    public Button galaxyViewButton;

    void OnEnable()
    {
        SolarSystemInstance = this;
        galaxyViewButton.interactable = false;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(mouseRay, out hit) && Input.GetMouseButtonDown(0))
        {
            Star star = Galaxy.GalaxyInstance.ReturnStarFromGameObject(hit.transform.gameObject);
            Debug.Log("This Star is Called: " + star.starName + "\n" + "It Has " + star.numberOfPlanets + " Planets");

            Galaxy.GalaxyInstance.DestroyGalaxy();
            CreateSolarSystem(star);
        }

    }

    // This method creates the solar system view after a star is clicked on in the galaxy view
    public void CreateSolarSystem(Star star)
    {
        Random.InitState(Galaxy.GalaxyInstance.seedNumber);

        Galaxy.GalaxyInstance.galaxyView = false;

        SpaceObjects.CreateSphereObject(star.starName, Vector3.zero, this.transform);

        for (int i = 0; i < star.planetList.Count; i++)
        {
            Planet planet = star.planetList[i];

            Vector3 planetPos = PositionMath.PlanetPosition(i);

            SpaceObjects.CreateSphereObject(planet.planetName, planetPos, this.transform);
        }

        galaxyViewButton.interactable = true;

    }

    public void DestroySolarSystem()
    {
        while (transform.childCount > 0)
        {
            Transform go = transform.GetChild(0);
            go.SetParent(null);
            Destroy(go.gameObject);
        }

        galaxyViewButton.interactable = false;

    }

    /*
   Copyright Shadowplay Coding 2017 - see www.shadowplaycoding.com for licensing details
   Removing this comment forfits any rights given to the user under licensing.
   */
}
