using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBase {

    public List<Ship> buildCue;

	public float currentProduction;

    public StarBase()
    {
        this.buildCue = new List<Ship>();
		this.currentProduction = 0f;
    }

    public void AddShipToBuildCue(Ship ship)
    {
        buildCue.Add(ship);
    }

    public void RemoveShipFromBuildCue(Ship ship)
    {
        buildCue.Remove(ship);
    }

    /*
    Copyright Shadowplay Coding 2017 - see www.shadowplaycoding.com for licensing details
    Removing this comment forfits any rights given to the user under licensing.
    */

}
