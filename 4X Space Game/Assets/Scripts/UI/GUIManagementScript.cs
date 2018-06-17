using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GUIManagementScript : MonoBehaviour {

    public static GUIManagementScript GUIManagerInstance;

    public List<GameObject> namePlates;

    public GameObject planetPanel;
    public GameObject starBasePanel;

	GameObject content;

    void OnEnable()
    {
        GUIManagerInstance = this;
        namePlates = new List<GameObject>();
    }

	void Awake()
	{
		content = starBasePanel.GetComponentInChildren<VerticalLayoutGroup>().gameObject;
	}

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        if(namePlates.Count > 0)
        {
            if (CameraController.cameraController.zoomLevel < 0.4)
            {
                for (int i = 0; i < namePlates.Count; i++)
                {
                    namePlates[i].transform.LookAt(Camera.main.transform);
                    float rotY = namePlates[i].transform.localEulerAngles.y + 180f;
                    namePlates[i].transform.localEulerAngles = new Vector3(
                        -namePlates[i].transform.localEulerAngles.x,
                        rotY,
                        -namePlates[i].transform.localEulerAngles.z);
                }
            }
        }

	}

	public void UpdateShipProductionUI()
	{
		Planet planet = SolarSystem.SolarSystemInstance.currentPlanet;

		float totalProduction = 0;

		Debug.ClearDeveloperConsole();

		if (planet.production > 0)
		{
			for (int i = 0; i < content.transform.childCount; i++)
			{
				Text[] texts = content.transform.GetChild(i).GetComponentsInChildren<Text>();

				Ship ship = planet.starBase.buildCue[i];

				int numberOfTurns = Mathf.CeilToInt(((ship.productionValue - planet.starBase.currentProduction) + totalProduction) / planet.production);

				texts[texts.Length - 1].text = numberOfTurns.ToString();

				totalProduction += ship.productionValue;
			}
		}
	}

	public void RemoveButtonFromCue()
	{
		Transform topButton = content.transform.GetChild(0);
		topButton.transform.SetParent(null);
		Destroy(topButton.gameObject);	
	}

	/*
    Copyright Shadowplay Coding 2017 - see www.shadowplaycoding.com for licensing details
    Removing this comment forfits any rights given to the user under licensing.
    */
}
