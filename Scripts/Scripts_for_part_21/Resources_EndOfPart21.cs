using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources {

    public int credits { get; set; }
    public int minerals { get; set; }
    public int food { get; set; }

    public Resources(int credits, int minerals, int food)
    {
        this.credits = credits;
        this.minerals = minerals;
        this.food = food;
    }

    // Adds an amount to a resource
    // 0 = all resources
    // 1 = credits
    // 2 = minerals
    // 3 = food
    public void AddResource(int resource, int amount)
    {
        switch (resource) {
            case 0:
                this.credits = this.credits + amount;
                this.minerals = this.minerals + amount;
                this.food = this.food + amount;
                break;
            case 1:
                this.credits = this.credits + amount;
                break;
            case 2:
                this.minerals = this.minerals + amount;
                break;
            case 3:
                this.food = this.food + amount;
                break;
            default:
                Debug.LogError("Unknown Resource!");
                break;
        }
    }

    // Subtracts an amount to a resource
    // 0 = all resources
    // 1 = credits
    // 2 = minerals
    // 3 = food
    public void SubtractResource(int resource, int amount)
    {
        switch (resource)
        {
            case 0:
                this.credits = this.credits - amount;
                this.minerals = this.minerals - amount;
                this.food = this.food - amount;
                break;
            case 1:
                this.credits = this.credits - amount;
                break;
            case 2:
                this.minerals = this.minerals - amount;
                break;
            case 3:
                this.food = this.food - amount;
                break;
            default:
                Debug.LogError("Unknown Resource!");
                break;
        }
    }

    /*
    Copyright Shadowplay Coding 2017 - see www.shadowplaycoding.com for licensing details
    Removing this comment forfits any rights given to the user under licensing.
    */
}
