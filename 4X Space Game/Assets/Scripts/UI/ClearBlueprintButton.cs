using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearBlueprintButton : MonoBehaviour {

	Instantiation ins;

	void Start()
	{
		ins = GetComponentInParent<Instantiation>();
	}

	public void DestroyObject()
	{
		ins.RemoveObject(this.gameObject);
		Destroy(this.gameObject);
	}
}
