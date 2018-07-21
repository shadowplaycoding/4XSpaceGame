using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour {

	public static TurnManager turnManagerInstance;

	public FleetManager fm;

	public Button endTurnButton;
	public Text turnText;

	int turnNumber;

	void Awake()
	{
		turnManagerInstance = this;
	}

	// Use this for initialization
	void Start () {
		turnNumber = 0;
	}

	public void EndTurn()
	{
		endTurnButton.interactable = false;

		UpdateTurnText();

		ApplyProduction();
		ApplyResources();

		endTurnButton.interactable = true;
	}

	void UpdateTurnText()
	{
		turnNumber += 1;

		turnText.text = "Turn: " + turnNumber;
	}

	void ApplyProduction()
	{
		for(int i = 0; i < PlayerManager.PlayerManagerInstance.ownedPlanets.Count; i++)
		{
			Planet planet = PlayerManager.PlayerManagerInstance.ownedPlanets[i];

			Debug.Log(PlayerManager.PlayerManagerInstance.ownedPlanets[i].planetName);

			Debug.Log("Current Production Is: " + planet.starBase.currentProduction);

			if (planet.starBase != null && planet.starBase.buildCue.Count !=0)
			{
				planet.starBase.currentProduction += planet.production;

				if (planet.starBase.currentProduction >= planet.starBase.buildCue[0].productionValue)
				{
					float difference = planet.starBase.currentProduction - planet.starBase.buildCue[0].productionValue;
					planet.starBase.currentProduction = difference;

					fm.BuildShip(planet.starBase.buildCue[0]);
					planet.starBase.buildCue.Remove(planet.starBase.buildCue[0]);
					GUIManagementScript.GUIManagerInstance.RemoveButtonFromCue();
				}

				GUIManagementScript.GUIManagerInstance.UpdateShipProductionUI();
			}
		}

	}

	void ApplyResources()
	{
		for(int i = 0; i < PlayerManager.PlayerManagerInstance.ownedPlanets.Count; i++)
		{
			Planet planet = PlayerManager.PlayerManagerInstance.ownedPlanets[i];

			PlayerManager.PlayerManagerInstance.playerResources.AddResource(ResourceType.Food, planet.planetResources.food);
			PlayerManager.PlayerManagerInstance.playerResources.AddResource(ResourceType.Minerals, planet.planetResources.minerals);
			PlayerManager.PlayerManagerInstance.playerResources.AddResource(ResourceType.Credits, planet.planetResources.credits);

			Debug.Log("Credits: " + PlayerManager.PlayerManagerInstance.playerResources.credits 
				+ " Minerals: " + PlayerManager.PlayerManagerInstance.playerResources.minerals 
				+ " Food: " + PlayerManager.PlayerManagerInstance.playerResources.food);
		}
	}

	/*
	Copyright Shadowplay Coding 2018 - see www.shadowplaycoding.com for licensing details
	Removing this comment forfits any rights given to the user under licensing.
	*/
}
