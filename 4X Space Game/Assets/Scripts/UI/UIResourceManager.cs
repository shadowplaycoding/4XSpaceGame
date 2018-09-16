using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResourceManager : MonoBehaviour {

	public static UIResourceManager instance;

	public Text creditsText;
	public Text mineralsText;
	public Text foodText;

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start() {

	}

	public void UpdateText(Text text, int amount)
	{
		text.text = amount.ToString();
	}

	/*
	Copyright Shadowplay Coding 2018 - see www.shadowplaycoding.com for licensing details
	Removing this comment forfits any rights given to the user under licensing.
	*/

}
