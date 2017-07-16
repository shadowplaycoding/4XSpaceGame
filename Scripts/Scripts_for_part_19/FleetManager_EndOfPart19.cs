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
        Ship ship = new Ship("Ship " + shipCount, 10, 10);
        Fleet fleet = new Fleet("Fleet " + (fleetList.Count + 1), ship);

        fleetList.Add(fleet);

        GameObject shipObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        shipObject.transform.position = PositionMath.RandomPosition(-50, 50);
        shipObject.transform.parent = this.transform;

        fleetToObjectMap.Add(fleet, shipObject);
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
