using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	//Movement stuff
	public float maxJumpHeight = 2;
	public float minJumpHeight = 1;
	public float timeToJumpApex = 0.5f;
	public float runSpeed = 12;
	public float accelerationTimeAirborne = 0.2f;
	public float accelerationTimeGrounded = 0.1f;

	[HideInInspector]
	public float speed01 = 0;

	//private movement stuff
	float gravity;
	float maxJumpVelocity;
	float minJumpVelocity;

	[HideInInspector]
	public Vector3 velocity;
	float velocityXSmoothing;
	MovementController controller;
	private PlayerControl pc;

	void Start ()
	{
		controller = this.GetComponent<MovementController>();
		gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity * timeToJumpApex);
		minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
		pc = this.GetComponent<PlayerControl> ();
	}

	float targetVelocityX = 0;

	[HideInInspector]
	public float input = 0;

	void Update ()
	{
		input = Input.GetAxisRaw("Horizontal");
		if (pc.dying)
			input = 0;

		//Hitting your head, the ground, max slope falling
		if (controller.collisionInfo.above || controller.collisionInfo.below || transform.position.y > 2.5)
		{
			velocity.y = 0;
		}

		if ((Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.Space) && !pc.dying)
			&& controller.collisionInfo.below)
		{
			velocity.y = maxJumpVelocity;
		}
		//Small Jump
		else if ((Input.GetKeyUp (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.Space) && !pc.dying)
			&& velocity.y > minJumpVelocity)
		{
			velocity.y = minJumpVelocity;
		}
		//Walk or run?
		targetVelocityX = input * runSpeed;
		//Smooth damp movement
		velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing,
			controller.collisionInfo.below ? accelerationTimeGrounded : accelerationTimeAirborne);

		speed01 = Mathf.Abs(velocity.x / runSpeed);

		//Gravity
		velocity.y += gravity * Time.deltaTime;
		//Actually move
		controller.Move(velocity * Time.deltaTime);
	}
}