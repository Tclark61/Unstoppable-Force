using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float speed = 10f;
	public float stop = 0f;
	public float playerJump = 5;

	public Rigidbody2D body;
	public Vector3 velocity;
	public float smoothTime = 0.3f;

	public float speed01 = 0;
	public bool grounded = false;

	public Transform otherSpot;

	// Use this for initialization
	public void Start () {
		body = GetComponent<Rigidbody2D> ();
		//gameObject = GameObject.FindObjectOfType (typeof(PowerUp)) as PowerUp;
	}
	
	// Update is called once per frame - animation shit
	void Update () {
		velocity.x = Input.GetAxis("Horizontal");
	    // velocity.y = Input.GetAxis("Vertical");
		if (Input.GetButtonDown ("Jump") && grounded){
			Jump ();
		}

		speed01 = Mathf.Abs (velocity.x);
		velocity = velocity * speed * Time.deltaTime;
		body.MovePosition(transform.position + velocity);
	}

	void Jump()
	{
		grounded = false;
		Vector2 temp = GetComponent<Rigidbody2D> ().velocity;
		temp.y += playerJump;
		GetComponent<Rigidbody2D> ().velocity = temp;
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		grounded = true;
	}

	//void OnCollisionExit2D(Collision2D other)
	//{
	//	grounded = false;
	//}

}
