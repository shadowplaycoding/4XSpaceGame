using UnityEngine;
using System.Collections;

public class Galaxy : MonoBehaviour {

    // TODO: Have these values import from user settings
    public int numberOfStars = 300;
    public int minimumRadius = 0;
    public int maximumRadius = 100;

    public float minDistBetweenStars;

	// Use this for initialization
	void Start () {

        if (minimumRadius > maximumRadius)
        {
            int tempValue = maximumRadius;
            maximumRadius = minimumRadius;
            minimumRadius = tempValue;
        }

        int failCount = 0;

        for (int i = 0; i < numberOfStars; i++)
        {

            Star starData = new Star("Star" + i, Random.Range(1, 10));
            Debug.Log("Created " + starData.starName + " with " + starData.numberOfPlanets + " planets");

            float distance = Random.Range(minimumRadius, maximumRadius);
            float angle = Random.Range(0, 2 * Mathf.PI);

            Vector3 cartPosition = new Vector3(distance * Mathf.Cos(angle), 0, distance * Mathf.Sin(angle));

            Collider[] positionCollider = Physics.OverlapSphere(cartPosition, minDistBetweenStars);

            if (positionCollider.Length == 0)
            {
                GameObject starGO = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                starGO.name = starData.starName;
                starGO.transform.position = cartPosition;
                failCount = 0;
            }
            else
            {
                i--;
                failCount++;
            }

            if (failCount > numberOfStars)
            {
                Debug.LogError("Could not fit all the stars in the galaxy. Distance between stars too big!");
                break;
            }
        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /*
    Copyright Shadowplay Coding 2016 - see www.shadowplaycoding.com for licensing details
    Removing this comment forfits any rights given to the user under licensing.
    */
}
