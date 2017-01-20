using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalFlock : MonoBehaviour {
	
	public GameObject ToasterPrefab;
	public GameObject ToastPrefab;

	public static float tanksize = 2.5f;

	public static int numFliers = 10;
	static int ToasterFliers = 20;

	public static GameObject[] toasters = new GameObject[ToasterFliers];

	// Use this for initialization
	void Start () {
		//instantiate numFlyers number of prefab in the toasters array, at random positions in the tank.
		// get this array from within the Flock.cs script, to apply flocking behavior..
		for (int i = 0; i <ToasterFliers; i++) {
			Vector3 pos = new Vector3 (Random.Range (0, 1),
				Random.Range (0, 1),
				              Random.Range (0, 1));
			toasters [i] = (GameObject)Instantiate (ToasterPrefab, pos, Quaternion.identity);
		}			
	}
}
