using UnityEngine;
using System.Collections;

public class Flock : MonoBehaviour {

	public float speed = 0.001f;
	float rotationSpeed = 3.0f;
	Vector3 averageHeading;
	Vector3 averagePosition;
	float neighborDistance = 1.0f;

	bool turning = false;

	// Use this for initialization
	void Start () {
		speed = Random.Range(0.01f,0.3f);
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (transform.position, Vector3.zero) >= GlobalFlock.tanksize) {
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
				FlockRules();
			}		
		
		transform.Translate (0, 0, Time.deltaTime * speed);
	}

	void FlockRules() {
		//reference array of toasters from GlobalFlock to apply the rules to them
		GameObject[] gos;
		gos = GlobalFlock.toasters;

		Vector3 vcenter = Vector3.zero;
		Vector3 vavoid = Vector3.zero;

		// group speed
		float gspeed = 0.001f;

		// goal position that the groups will head towards
		// this is set with vive controller trigger pull, in ToastGun.cs
		Vector3 goalPos = ToastGun.goalPos;

		float dist;

		int groupSize = 0;
		foreach (GameObject go in gos) {
			if (go != this.gameObject) {
				dist = Vector3.Distance (go.transform.position, this.transform.position);
				if (dist <= neighborDistance) {
					vcenter += go.transform.position;
					groupSize++;

					if (dist < 0.09f) {
						vavoid = vavoid + (this.transform.position - go.transform.position);
					}

					Flock anotherFlock = go.GetComponent<Flock> ();
					gspeed = gspeed + anotherFlock.speed;
				}
			}
		}

		if (groupSize > 0) {
			vcenter = vcenter / groupSize + (goalPos - this.transform.position);
			speed = gspeed / groupSize;

			Vector3 direction = (vcenter + vavoid) - transform.position;
			if(direction != Vector3.zero)
				transform.rotation = Quaternion.Slerp(transform.rotation,
					Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
			
		}
	
	}

}