using UnityEngine;
using System.Collections;

public class animationSpeed : MonoBehaviour {

	public Animation anim;

	void Start()
	{
		// play animation at random speed
		anim["toasterFlapping"].speed = Random.Range(0.5f,1.5f);
	}
}
