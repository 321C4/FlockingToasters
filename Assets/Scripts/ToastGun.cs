using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class ToastGun : MonoBehaviour {

	SteamVR_TrackedObject trackedObj;
	public GameObject ToastPrefab;
	public static Vector3 goalPos = Vector3.zero;

	public static int numFliers = 10;
	public static List<GameObject> allToast;

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	void Start(){
		allToast = new List<GameObject>(numFliers);
	}
	
	// Update is called once per frame
	void Update () {
		var device = SteamVR_Controller.Input ((int)trackedObj.index);
		if (device.GetPressDown (SteamVR_Controller.ButtonMask.Trigger)) {
			goalPos = gameObject.transform.position;
			allToast.Add((GameObject)Instantiate (ToastPrefab, transform.position, transform.rotation));
			Debug.Log ("spawn toast!");
		}
	}
}
