using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class toastBehavior : MonoBehaviour {

	public float speed = 0.1f;
	float rotationSpeed = 3.0f;
	Vector3 averageHeading;
	Vector3 averagePosition;
	float neighborDistance = 3.0f;
	public static float tanksize = 2.5f;

	bool turning = false;

	// Use this for initialization
	void Start () {
		speed = Random.Range(0.01f,0.3f);

	}

	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (transform.position, Vector3.zero) >= tanksize) {
			turning = true;
		} 
		else
			turning = false;
		if (turning) {
			Vector3 direction = Vector3.zero - transform.position;
			transform.rotation = Quaternion.Slerp(transform.rotation,
				Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);

			speed = Random.Range(0.001f, 0.03f);
		}
		else {
			if (Random.Range (0, 5) < 1)
				ApplyRules();
		}		

		transform.Translate (0, 0, Time.deltaTime * speed);
	}

	void ApplyRules() {
		List<GameObject> gos;
		gos = ToastGun.allToast;

		Vector3 vCenter = Vector3.zero;
		Vector3 vAvoid = Vector3.zero;
		float gspeed = 0.001f;

		Vector3 goalPos = ToastGun.goalPos;

		float dist;

		int groupSize = 0;
		foreach (GameObject go in gos) {
			if (go != this.gameObject) {
				dist = Vector3.Distance (go.transform.position, this.transform.position);
				if (dist <= neighborDistance) {
					vCenter += go.transform.position;
					groupSize++;

					if (dist < 0.009f) {
						vAvoid = vAvoid + (this.transform.position - go.transform.position);
					}

					Flock anotherFlock = go.GetComponent<Flock> ();
					gspeed = gspeed + anotherFlock.speed;
				}
			}
		}

		if (groupSize > 0) {
			vCenter = vCenter / groupSize + (goalPos - this.transform.position);
			speed = gspeed / groupSize;

			Vector3 direction = (vCenter + vAvoid) - transform.position;
			if(direction != Vector3.zero)
				transform.rotation = Quaternion.Slerp(transform.rotation,
					Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);

		}

	}

}