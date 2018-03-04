using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour {

	public static TurnManager turnManagerInstance;

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

		}
	}

	/*
	Copyright Shadowplay Coding 2018 - see www.shadowplaycoding.com for licensing details
	Removing this comment forfits any rights given to the user under licensing.
	*/
}
