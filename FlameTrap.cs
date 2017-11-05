using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameTrap : MonoBehaviour
{

	private Animator state;
	private BoxCollider2D box;
	private Light light;

	//public float speed = 0.667f;

	void Start ()
	{
		state = this.GetComponent<Animator> ();
		box = this.GetComponent<BoxCollider2D> ();
		light = this.GetComponent<Light> ();
		//state.GetCurrentAnimatorStateInfo (0).speed = speed;
	}

	// Update is called once per frame
	void Update ()
	{
		if ((state.GetCurrentAnimatorStateInfo (0).normalizedTime % 1) > 0.4)
		{
			box.enabled = true;
			light.intensity = 20;
		}
		else
		{
			box.enabled = false;
			light.intensity = 6;
		}
	}
}
