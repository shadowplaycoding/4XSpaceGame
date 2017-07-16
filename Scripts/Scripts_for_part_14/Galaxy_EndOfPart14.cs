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
    public int numberOfArms = 2;

    [Range(0,100)]
    public int percentageStarsCentre = 25;

    public float minDistBetweenStars;

    public string[] availablePlanetTypes = { "Barren", "Terran", "Gas Giant" };

    public Dictionary<Star, GameObject> starToObjectMap {get; protected set;}

    public static Galaxy GalaxyInstance;

    public bool galaxyView { get; set; }

    public GameObject selectionIcon;

    float percent;
    float starsInCentre;
    int starsInCentreRounded;

    float starsPerArm;
    int starsPerArmRounded;
    int difference;

    int starCount = 0;

    void OnEnable()
    {
        GalaxyInstance = this;
    }

    // Use this for initialization
    void Start () {

        SanityChecks();
        CreateSelectionIcon();
        //CreateGalaxy();
        CreateSpiralGalaxy();        
	
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
            string name = star.starName + " " + (star.planetList.Count + 1).ToString();

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
        InitializeGalaxy();

        int failCount = 0;

        for (int i = 0; i < numberOfStars; i++)
        {

            Star starData = new Star("Star" + i, Random.Range(1, 10));
            //Debug.Log("Created " + starData.starName + " with " + starData.numberOfPlanets + " planets");
            CreatePlanetData(starData);

            Vector3 cartPosition = PositionMath.RandomPosition(minimumRadius, maximumRadius);

            bool collision = PositionMath.CheckCollisions(minDistBetweenStars, cartPosition);

            if (!collision == true)
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

    // This method creates a spiral galaxy
    public void CreateSpiralGalaxy()
    {
        InitializeGalaxy();

        GalaxyMaths();

        CreateArms();
        CreateCentre();

    }

    // This method destroys all children of the Galaxy Manager object
    public void DestroyGalaxy()
    {
        while (transform.childCount > 0)
        {
            Transform go = transform.GetChild(0);
            go.SetParent(null);
            Destroy(go.gameObject);
        }

    }

    // This method creates the selection icon
    void CreateSelectionIcon()
    {
        selectionIcon = GameObject.Instantiate(selectionIcon);
        selectionIcon.transform.localScale = selectionIcon.transform.localScale * 2.5f;
        selectionIcon.SetActive(false);
    }

    // This method moves the selection icon when mouse moves over a new object
    public void MoveSelectionIcon(RaycastHit hit)
    {
        selectionIcon.SetActive(true);
        selectionIcon.transform.position = hit.transform.position;
        selectionIcon.transform.rotation = CameraController.currentAngle;
    }

    // This method initializes the galaxy by setting seed number, creating a new dictionary, setting galaxy view to true and setting star count to 0
    void InitializeGalaxy()
    {
        starToObjectMap = new Dictionary<Star, GameObject>();
        Random.InitState(seedNumber);
        galaxyView = true;
        starCount = 0;
    }

    // This method works out number of stars in arms and centre
    void GalaxyMaths()
    {
        percent = percentageStarsCentre / 100f;
        starsInCentre = percent * numberOfStars;
        starsInCentreRounded = Mathf.RoundToInt(starsInCentre);

        starsPerArm = (numberOfStars - starsInCentreRounded) / numberOfArms;
        starsPerArmRounded = Mathf.RoundToInt(starsPerArm);
        difference = numberOfStars - (starsPerArmRounded * numberOfArms) - starsInCentreRounded;

        maximumRadius = Mathf.RoundToInt((6 * numberOfArms) * Mathf.Sqrt(numberOfStars / numberOfArms) + minimumRadius);
    }

    Star CreateStarData(int starCount)
    {
        Star starData = new Star("Star " + starCount, Random.Range(1, 10));
        CreatePlanetData(starData);

        return starData;
    }

    void CreateStarObject(Star starData, Vector3 cartPosition)
    {
        GameObject starGO = SpaceObjects.CreateSphereObject(starData.starName, cartPosition, this.transform);
        starToObjectMap.Add(starData, starGO);
    }

    // This method creats the galaxy arms
    void CreateArms()
    {
        // Spawn Arms
        for (int i = 0; i < numberOfArms; i++)
        {
            for (int j = 0; j < starsPerArmRounded; j++)
            {
                Star starData = CreateStarData(starCount);
                starCount++;

                float armAngle = (((Mathf.PI * 2f) / numberOfArms) * i);
                float starAngle = (((Mathf.PI * 2f) / starsPerArmRounded) * j);

                float angle = PositionMath.SpiralAngle(armAngle, starAngle) + Random.Range(-Mathf.PI / (2 * numberOfArms), Mathf.PI / (2 * numberOfArms));
                float distance = (6 * numberOfArms) * Mathf.Sqrt(j + 1) + minimumRadius;

                Vector3 cartPosition = PositionMath.PolarToCart(distance, angle);

                int failCount = 0;

                bool collision = PositionMath.CheckCollisions(minDistBetweenStars, cartPosition);

                if (collision != true)
                {
                    CreateStarObject(starData, cartPosition);
                    failCount = 0;
                }
                else
                {
                    j--;
                    failCount++;
                }
                if (failCount > numberOfStars)
                {
                    break;
                }

            }

        }
    }

    // This method creates the galaxy centre
    void CreateCentre()
    {
        // Spawn Centre
        for (int k = 0; k < starsInCentreRounded + difference; k++)
        {
            Star starData = CreateStarData(starCount);
            starCount++;

            Vector3 cartPosition = PositionMath.RandomPosition(minimumRadius, minimumRadius + (numberOfArms * 20));

            bool collision = PositionMath.CheckCollisions(minDistBetweenStars, cartPosition);

            int failCount = 0;

            if (collision != true)
            {
                CreateStarObject(starData, cartPosition);
                failCount = 0;
            }
            else
            {
                k--;
                failCount++;
            }
            if (failCount > numberOfStars)
            {
                break;
            }

        }
    }
   /*
   Copyright Shadowplay Coding 2017 - see www.shadowplaycoding.com for licensing details
   Removing this comment forfits any rights given to the user under licensing.
   */

}
