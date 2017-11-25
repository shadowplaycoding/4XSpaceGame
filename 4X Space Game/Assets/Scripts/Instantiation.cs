using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiation : MonoBehaviour {

	public GameObject objectToInstantiate;

	List<GameObject> objectsInstantiated = new List<GameObject>();

	public void InstantiateObject()
	{
		GameObject GO = Instantiate(objectToInstantiate, this.transform);
		objectsInstantiated.Add(GO);
	}

	public void RemoveObject(GameObject GO)
	{
		if (objectsInstantiated.Contains(GO))
		{
			objectsInstantiated.Remove(GO);
		}
	}
}
