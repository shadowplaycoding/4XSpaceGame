using UnityEngine;
using System.Collections;

public class SolarSystem : MonoBehaviour {

    public static SolarSystem SolarSystemInstance;

    void OnEnable()
    {
        SolarSystemInstance = this;
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
        }

    }

    /*
   Copyright Shadowplay Coding 2017 - see www.shadowplaycoding.com for licensing details
   Removing this comment forfits any rights given to the user under licensing.
   */
}
