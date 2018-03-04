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
		endTurnButton.interactable = true;
	}

	void UpdateTurnText()
	{
		turnNumber += 1;

		turnText.text = "Turn: " + turnNumber;
	}

	void ApplyProduction()
	{
		for(int i = 0; i<PlayerManager.PlayerManagerInstance.ownedPlanets.Count; i++)
		{
			Planet planet = PlayerManager.PlayerManagerInstance.ownedPlanets[i];

			if (planet.starBase != null && planet.starBase.buildCue.Count !=0)
			{
				planet.starBase.currentProduction += planet.production;

				if (planet.starBase.currentProduction >= planet.starBase.buildCue[0].productionValue)
				{
					float difference = planet.starBase.currentProduction - planet.starBase.buildCue[0].productionValue;
					planet.starBase.currentProduction = difference;

					fm.BuildShip();
					planet.starBase.buildCue.Remove(planet.starBase.buildCue[0]);
					
				}
			}

		}
	}

	/*
	Copyright Shadowplay Coding 2018 - see www.shadowplaycoding.com for licensing details
	Removing this comment forfits any rights given to the user under licensing.
	*/
}
