using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Galaxy : MonoBehaviour {

    // TODO: Have these values import from user settings
    public int numberOfStars = 300;
    public int minimumRadius = 0;
    public int maximumRadius = 100;
    public int seedNumber = 100;

    public float minDistBetweenStars;

    public string[] availablePlanetTypes = { "Barren", "Terran", "Gas Giant" };

    public Dictionary<Star, GameObject> starToObjectMap {get; protected set;}

    public static Galaxy GalaxyInstance;

    public bool galaxyView { get; set; }

    public GameObject selectionIcon;

    void OnEnable()
    {
        GalaxyInstance = this;
    }

    // Use this for initialization
    void Start () {

        SanityChecks();
        CreateSelectionIcon();
        CreateGalaxy();        
	
	}

    // This method checks game logic to make sure things are correct 
    // before the galaxy is created
    void SanityChecks()
    {
        if (minimumRadius > maximumRadius)
        {
            int tempValue = maximumRadius;
            maximumRadius = minimumRadius;
            minimumRadius = tempValue;
        }
    }

    // This method creates all the planet data for a star
    void CreatePlanetData(Star star)
    {
        for (int i = 0; i < star.numberOfPlanets; i++)
        {
            string name = star.starName + (star.planetList.Count + 1).ToString();

            int random = Random.Range(1, 100);
            string type = "";
            
            if (random < 40)
            {
                type = availablePlanetTypes[0];
            }
            else if (40 <= random && random < 50)
            {
                type = availablePlanetTypes[1];
            }
            else
            {
                type = availablePlanetTypes[2];
            }

            Planet planetData = new Planet(name, type);
            //Debug.Log(planetData.planetName + " " + planetData.planetType );

            star.planetList.Add(planetData);

        }
    }

    public Star ReturnStarFromGameObject(GameObject go)
    {
        if (starToObjectMap.ContainsValue(go))
        {
            int index = starToObjectMap.Values.ToList().IndexOf(go);
            Star star = starToObjectMap.Keys.ToList()[index];

            return star;
        }
        else
        {
            return null;
        }
    }

    // This method creates a galaxy of stars and planet information.
    public void CreateGalaxy()
    {
        starToObjectMap = new Dictionary<Star, GameObject>();

        Random.InitState(seedNumber);

        galaxyView = true;

        int failCount = 0;

        for (int i = 0; i < numberOfStars; i++)
        {

            Star starData = new Star("Star" + i, Random.Range(1, 10));
            //Debug.Log("Created " + starData.starName + " with " + starData.numberOfPlanets + " planets");
            CreatePlanetData(starData);

            Vector3 cartPosition = PositionMath.RandomPosition(minimumRadius, maximumRadius);

            Collider[] positionCollider = Physics.OverlapSphere(cartPosition, minDistBetweenStars);

            if (positionCollider.Length == 0)
            {
                GameObject starGO = SpaceObjects.CreateSphereObject(starData.starName, cartPosition, this.transform);
                starToObjectMap.Add(starData, starGO);
                failCount = 0;
            }
            else
            {
                i--;
                failCount++;
            }

            if (failCount > numberOfStars)
            {
                Debug.LogError("Could not fit all the stars in the galaxy. Distance between stars too big!");
                break;
            }
        }
    }

    public void DestroyGalaxy()
    {
        while (transform.childCount > 0)
        {
            Transform go = transform.GetChild(0);
            go.SetParent(null);
            Destroy(go.gameObject);
        }

    }

    void CreateSelectionIcon()
    {
        selectionIcon = GameObject.Instantiate(selectionIcon);
        selectionIcon.transform.localScale = selectionIcon.transform.localScale * 2.5f;
        selectionIcon.SetActive(false);
    }

    public void MoveSelectionIcon(RaycastHit hit)
    {
        selectionIcon.SetActive(true);
        selectionIcon.transform.position = hit.transform.position;
        selectionIcon.transform.rotation = CameraController.currentAngle;
    }

   /*
   Copyright Shadowplay Coding 2017 - see www.shadowplaycoding.com for licensing details
   Removing this comment forfits any rights given to the user under licensing.
   */

}
