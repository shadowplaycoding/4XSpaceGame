using UnityEngine;
using System.Collections;


public class SpaceObjects {

    // This method creates a sphere object whether that be a planet or star.
	public static GameObject CreateSphereObject(string name, Vector3 position, Transform parent = null)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.name = name;
        sphere.transform.position = position;
        sphere.transform.parent = parent;
        sphere.isStatic = true;

        CreateNamePlate(sphere);

        return sphere;
    }

    // This method creates orbital graphics for planets in the solar system.
    public static GameObject CreateOrbitPath(GameObject orbitSprite, string name, int orbitNumber, Transform parent = null)
    {
        GameObject orbit = GameObject.Instantiate(orbitSprite);
        
        orbit.name = name;
        orbit.transform.localScale = orbit.transform.localScale * orbitNumber;
        orbit.transform.SetParent(parent);

        return orbit;
    }

    public static void CreateNamePlate(GameObject go)
    {
        TextMesh nameText = new GameObject(go.name + " Name Plate").AddComponent<TextMesh>();
        nameText.transform.SetParent(go.transform);
        nameText.text = go.name;
        nameText.transform.localPosition = new Vector3(0, -1.2f, 0);
        nameText.anchor = TextAnchor.MiddleCenter;
        nameText.alignment = TextAlignment.Center;
        nameText.color = Color.white;
        nameText.fontSize = 10;
        nameText.gameObject.isStatic = true;
    }

    /*
    Copyright Shadowplay Coding 2017 - see www.shadowplaycoding.com for licensing details
    Removing this comment forfits any rights given to the user under licensing.
    */

}
