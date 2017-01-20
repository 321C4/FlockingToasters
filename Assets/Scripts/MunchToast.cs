using UnityEngine;
using System.Collections;

public class MunchToast : MonoBehaviour {
	public AudioClip munch;
	AudioSource audio;

	void Start() {
//		audio = GetComponent<AudioSource>();
	}
	
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "toaster") {
			Destroy (gameObject);
			Debug.Log ("ate toast!");

	//		audio.PlayOneShot(munch, 0.7F);
		}
	}
}
