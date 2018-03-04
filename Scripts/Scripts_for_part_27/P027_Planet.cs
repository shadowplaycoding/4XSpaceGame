using UnityEngine;
using System.Collections;

public class Planet {

    public string planetName { get; set; }
    public string planetType { get; set; }

    public StarBase starBase;

    public bool planetColonised = false;

	public float production = 2;

    public Planet(string name, string type)
    {
        this.planetName = name;
        this.planetType = type;

        this.starBase = null;
    }

    /*
   Copyright Shadowplay Coding 2017 - see www.shadowplaycoding.com for licensing details
   Removing this comment forfits any rights given to the user under licensing.
   */

}
