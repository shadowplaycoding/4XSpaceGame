using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Star {

    public string starName { get; protected set; }
    public int numberOfPlanets { get; protected set; }

    public List<Planet> planetList;

    public Star(string name, int planets)
    {
        this.starName = name;
        this.numberOfPlanets = planets;

        this.planetList = new List<Planet>();
    }

    /*
   Copyright Shadowplay Coding 2016 - see www.shadowplaycoding.com for licensing details
   Removing this comment forfits any rights given to the user under licensing.
   */
}
