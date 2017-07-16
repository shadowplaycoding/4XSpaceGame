using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Galaxy : MonoBehaviour {

    // TODO: Have these values import from user settings
    public int numberOfStars = 300;
    public int minimumRadius = 0;
    public int maximumRadius = 100;

    public float minDistBetweenStars;

    public string[] availablePlanetTypes = { "Barren", "Terran", "Gas Giant" };

    // Use this for initialization
    void Start () {

        SanityChecks();

        int failCount = 0;

        for (int i = 0; i < numberOfStars; i++)
        {

            Star starData = new Star("Star" + i, Random.Range(1, 10));
            //Debug.Log("Created " + starData.starName + " with " + starData.numberOfPlanets + " planets");
            CreatePlanetData(starData);

            Vector3 cartPosition = RandomPosition();

            Collider[] positionCollider = Physics.OverlapSphere(cartPosition, minDistBetweenStars);

            if (positionCollider.Length == 0)
            {
                GameObject starGO = CreateSphereObject(starData, cartPosition);
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

    // This method creates a random polar coordinate then converts and returns it as a Cartesian coordinate
    Vector3 RandomPosition() {

        float distance = Random.Range(minimumRadius, maximumRadius);
        float angle = Random.Range(0, 2 * Mathf.PI);

        Vector3 cartPosition = new Vector3(distance * Mathf.Cos(angle), 0, distance * Mathf.Sin(angle));

        return cartPosition;
    }

    // This method creates a sphere object using the built in sphere model in unity
    GameObject CreateSphereObject(Star starData, Vector3 cartPosition)
    {
        GameObject starGO = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        starGO.name = starData.starName;
        starGO.transform.position = cartPosition;

        return starGO;
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
            Debug.Log(planetData.planetName + " " + planetData.planetType );

            star.planetList.Add(planetData);

        }
    }

    /*
   Copyright Shadowplay Coding 2016 - see www.shadowplaycoding.com for licensing details
   Removing this comment forfits any rights given to the user under licensing.
   */

}
