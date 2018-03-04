using UnityEngine;
using System.Collections;

public class Ship {

    public string shipName { get; set; }
    public int hullPoints { get; set; }
    public int shieldPoints { get; set; }

    public int maxHull { get; protected set; }
    public int maxShields { get; protected set; }

	public float productionValue = 10.0f;
	
    public Ship(string name, int maxHull, int maxShields)
    {
        this.shipName = name;
        this.hullPoints = maxHull;
        this.maxHull = maxHull;
        this.shieldPoints = maxShields;
        this.maxShields = maxShields;
    }

    /*
    Copyright Shadowplay Coding 2017 - see www.shadowplaycoding.com for licensing details
    Removing this comment forfits any rights given to the user under licensing.
    */

}
