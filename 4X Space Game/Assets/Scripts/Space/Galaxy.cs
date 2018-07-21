using UnityEngine;
using UnityEngine.UI;
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

    public TextAsset starNames;

    public Material starOwnedMaterial;

    float percent;
    float starsInCentre;
    int starsInCentreRounded;

    float starsPerArm;
    int starsPerArmRounded;
    int difference;

    int starCount = 0;
    int starOwned;

    List<string> availableStarNames;

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
            string name = star.starName + " " + RomanNumerals.RomanNumeralGenerator(star.planetList.Count + 1);

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

        GUIManagementScript.GUIManagerInstance.namePlates.Clear();

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
        availableStarNames = TextAssetManager.TextToList(starNames);
    }

    // This method works out number of stars in arms and centre
    void GalaxyMaths()
    {
        percent = percentageStarsCentre / 100f;
        starsInCentre = percent * numberOfStars;
        starsInCentreRounded = Mathf.RoundToInt(starsInCentre);
        //Debug.Log(starsInCentreRounded);

        starsPerArm = (numberOfStars - starsInCentreRounded) / numberOfArms;
        starsPerArmRounded = Mathf.RoundToInt(starsPerArm);
        //Debug.Log(starsPerArmRounded);
        difference = numberOfStars - (starsPerArmRounded * numberOfArms) - starsInCentreRounded;
        //Debug.Log(difference);

        maximumRadius = Mathf.RoundToInt((6 * numberOfArms) * Mathf.Sqrt(numberOfStars / numberOfArms) + minimumRadius);

        starOwned = Random.Range(0, numberOfStars - 1);
    }

    Star CreateStarData(int starCount)
    {
        string name;
        int randomIndex;

        if (availableStarNames.Count > 0)
        {
            randomIndex = Random.Range(0, availableStarNames.Count - 1);
            name = availableStarNames[randomIndex];
            availableStarNames.RemoveAt(randomIndex);
        }
        else
        {
            name = "Star " + starCount;
        }     
        
        Star starData = new Star(name, Random.Range(1, 10));
        CreatePlanetData(starData);
                
        return starData;
    }

    void CreateStarObject(Star starData, Vector3 cartPosition)
    {
        GameObject starGO = SpaceObjects.CreateSphereObject(starData.starName, cartPosition, this.transform);

        SetStarMaterial(starGO, starData);

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
                float armAngle = (((Mathf.PI * 2f) / numberOfArms) * i);
                float starAngle = (((Mathf.PI * 2f) / starsPerArmRounded) * j);

                float angle = PositionMath.SpiralAngle(armAngle, starAngle) + Random.Range(-Mathf.PI / (2 * numberOfArms), Mathf.PI / (2 * numberOfArms));
                float distance = (6 * numberOfArms) * Mathf.Sqrt(j + 1) + minimumRadius;

                Vector3 cartPosition = PositionMath.PolarToCart(distance, angle);

                int failCount = 0;

                bool collision = PositionMath.CheckCollisions(minDistBetweenStars, cartPosition);

                if (collision != true)
                {
                    // Data for Star
                    Star starData = CreateStarData(starCount);
                    starData.starPosition = cartPosition;
                    starCount++;
                    SetStarOwned(starData);

                    // Object for Star
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
        for (int k = 0; k < (starsInCentreRounded + difference); k++)
        {
            Vector3 cartPosition = PositionMath.RandomPosition(minimumRadius, minimumRadius + (numberOfArms * 20));

            bool collision = PositionMath.CheckCollisions(minDistBetweenStars, cartPosition);

            int failCount = 0;

            if (collision != true)
            {
                // Data for Star
                Star starData = CreateStarData(starCount);
                starData.starPosition = cartPosition;
                starCount++;
                SetStarOwned(starData);

                // Object for Star
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

    public void ResetGalaxy()
    {
        List<Star> starList = starToObjectMap.Keys.ToList<Star>();
        starToObjectMap = new Dictionary<Star, GameObject>();

        for (int i = 0; i < starList.Count; i++)
        {
            Star star = starList[i];
            GameObject starGO = SpaceObjects.CreateSphereObject(star.starName, star.starPosition, this.transform);
            SetStarMaterial(starGO, star);
            starToObjectMap.Add(star, starGO);
        }

        GalaxyInstance.galaxyView = true;
        
    }

    void SetStarOwned(Star starData)
    {
        if (starCount == starOwned)
        {
            starData.starOwned = true;
            starData.planetList[0].planetColonised = true;
			starData.planetList[0].starBase = new StarBase();
			PlayerManager.PlayerManagerInstance.ownedPlanets.Add(starData.planetList[0]);
        }
    }

    void SetStarMaterial(GameObject starGO, Star starData)
    {
        if(starData.starOwned == true)
        {
            starGO.GetComponent<MeshRenderer>().material = starOwnedMaterial;
        }
    }


   /*
   Copyright Shadowplay Coding 2017 - see www.shadowplaycoding.com for licensing details
   Removing this comment forfits any rights given to the user under licensing.
   */

}
