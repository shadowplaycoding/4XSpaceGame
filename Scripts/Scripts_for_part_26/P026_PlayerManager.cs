using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public static PlayerManager PlayerManagerInstance;

    public Resources playerResources { get; set; }

	public List<Planet> ownedPlanets;

    void OnEnable()
    {
        PlayerManagerInstance = this;
    }

	void Awake()
	{
		ownedPlanets = new List<Planet>();
	}

	// Use this for initialization
	void Start () {

        // Start game with 100 of each resource 
        playerResources = new Resources(100, 100, 100);
        Debug.Log("Starting the Game with: " + playerResources.credits + " Credits, " + playerResources.minerals + " Minerals and " + playerResources.food + " Food.");
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /*
    Copyright Shadowplay Coding 2017 - see www.shadowplaycoding.com for licensing details
    Removing this comment forfits any rights given to the user under licensing.
    */
}
