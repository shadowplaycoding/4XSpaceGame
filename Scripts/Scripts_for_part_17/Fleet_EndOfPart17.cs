using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fleet {

    public List<Ship> ships;
    public string fleetName { get; set; } 

    public Fleet(string name, Ship ship)
    {
        this.fleetName = name;
        ships = new List<Ship>();
        ships.Add(ship);
    }

    public void AddShip(Ship ship)
    {
        ships.Add(ship);
    }

    public void RemoveShip(Ship ship)
    {
        ships.Remove(ship);
    }

    /*
    Copyright Shadowplay Coding 2017 - see www.shadowplaycoding.com for licensing details
    Removing this comment forfits any rights given to the user under licensing.
    */

}
