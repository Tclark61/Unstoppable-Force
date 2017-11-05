using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
	Animator trap;
	BoxCollider2D box;
	public float delayBeforeReactivation = 3;

	private bool canSpike = true;

	void Start ()
	{
		trap = transform.Find ("SpikeTrap").GetComponent<Animator>();
		box = transform.Find ("SpikeTrap").GetComponent<BoxCollider2D> ();
		box.enabled = false;
		//Debug.Log (box);
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.tag == "Player" && canSpike)
		{
			trap.SetTrigger ("Spike");
			StartCoroutine (Delay ());
		}
	}

	IEnumerator Delay ()
	{
		canSpike = false;
		yield return new WaitForSeconds (0.2f);
		box.enabled = true;
		yield return new WaitForSeconds (0.4f);
		box.enabled = false;

		yield return new WaitForSeconds (delayBeforeReactivation - 1);

		canSpike = true;
	}
}
