using UnityEngine;
using System.Collections;

public class SpaceObjects {

    // This method creates a sphere object whether that be a planet or star
	public static GameObject CreateSphereObject(string name, Vector3 position, Transform parent = null)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.name = name;
        sphere.transform.position = position;
        sphere.transform.parent = parent;

        return sphere;
    }

    /*
    Copyright Shadowplay Coding 2017 - see www.shadowplaycoding.com for licensing details
    Removing this comment forfits any rights given to the user under licensing.
    */

}
