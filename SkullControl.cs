using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullControl : MonoBehaviour
{
	public float delay = 3;
	public int numTries = 5;
	public GameObject explosion;

	private Vector3 dir;

	private Transform player;
	private Animator anim;

	private bool hunting = false;
	public bool triggered = false;
	private bool moving = false;

	void Start ()
	{
		player = GameObject.Find ("Player").transform;
		anim = this.GetComponent<Animator> ();
	}
		
	void Update ()
	{
		if (triggered && !hunting)
		{
			hunting = true;
			StartCoroutine (delayedAttacks ());
		}
		if (moving)
		{
			transform.Translate (dir * Time.deltaTime);
			Vector3 temp = transform.localScale;
			temp.x = (dir.x < 0) ? -1 : 1;
			transform.localScale = temp;
		}
		anim.SetBool ("isMoving", moving);
	}

	IEnumerator delayedAttacks ()
	{
		for (int i = 0; i < numTries; i++)
		{
			dir = -new Vector3 (transform.position.x - player.position.x, 
									    transform.position.y - player.position.y, 0).normalized;
			moving = true;
			yield return new WaitForSeconds (1);
			moving = false;

			yield return new WaitForSeconds (delay);
		}
		Die ();
	}

	void Die ()
	{
		Instantiate (explosion, transform.position, transform.rotation);
		Destroy (gameObject);
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.name == "Player")
			triggered = true;
	}
}
