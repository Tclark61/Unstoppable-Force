using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	private Transform player;
	public float cameraFollowRadius = 0.5f;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.Find ("Player").transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 temp = transform.position;

		if (temp.x - player.position.x < -cameraFollowRadius)
			temp.x = player.position.x - cameraFollowRadius;
		else if (temp.x - player.position.x > cameraFollowRadius)
			temp.x = player.position.x + cameraFollowRadius;
			
		//temp.x = player.position.x;

		transform.position = temp;
	}
}
