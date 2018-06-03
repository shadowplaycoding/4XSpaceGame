using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FleetManager : MonoBehaviour {

    public List<Fleet> fleetList;

    public Dictionary<Fleet, GameObject> fleetToObjectMap;

    int shipCount;

	// Use this for initialization
	void Start () {
        fleetList = new List<Fleet>();
        fleetToObjectMap = new Dictionary<Fleet, GameObject>();	
	}

    public void BuildShip()
    {
        if (PlayerManager.PlayerManagerInstance.playerResources.credits >=10
            &&
            PlayerManager.PlayerManagerInstance.playerResources.minerals >= 10)
        {
            // Subtract 10 Credits
            PlayerManager.PlayerManagerInstance.playerResources.SubtractResource(1, 10);

            // Subtract 10 Minerals
            PlayerManager.PlayerManagerInstance.playerResources.SubtractResource(2, 10);

            Debug.Log("You now have: " + PlayerManager.PlayerManagerInstance.playerResources.credits
                + " Credits and " + PlayerManager.PlayerManagerInstance.playerResources.minerals + " Minerals");

            Ship ship = new Ship("Ship " + shipCount, 10, 10);
			SolarSystem.SolarSystemInstance.currentPlanet.starBase.buildCue.Add(ship);

			GUIManagementScript.GUIManagerInstance.UpdateShipProductionUI();


            Fleet fleet = new Fleet("Fleet " + (fleetList.Count + 1), ship);

            fleetList.Add(fleet);

            GameObject shipObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            shipObject.transform.position = PositionMath.RandomPosition(-50, 50);
            shipObject.transform.localScale = new Vector3(0.2f,0.2f,0.2f);
            shipObject.transform.parent = this.transform;

            fleetToObjectMap.Add(fleet, shipObject);

        }
        else
        {
            Debug.Log("Not Enough Resourses!");
        }
    }

    public void DestroyShip()
    {

    }

    public void DisableFleets()
    {
        this.gameObject.SetActive(false);
    }

    public void EnableFleets()
    {
        this.gameObject.SetActive(true);
    }

    /*
    Copyright Shadowplay Coding 2017 - see www.shadowplaycoding.com for licensing details
    Removing this comment forfits any rights given to the user under licensing.
    */

}
