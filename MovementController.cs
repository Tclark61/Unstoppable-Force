using UnityEngine;
using System.Collections;

public class MovementController : RaycastController
{
	public struct CollisionInfo
	{
		public bool above, below, left, right;
		public bool climbingSlope, descendingSlope;
		public bool slidingDownMaxSlope;
		public float slopeAngle, slopeAngleOld;
		public Vector2 slopeNormal;
		public Vector3 velocityOld;
		public bool movingUp;

		public void Reset ()
		{
			above = below = left = right = false;
		}
	}

	public LayerMask collisionMask;
	public CollisionInfo collisionInfo;
	
	public void Move (Vector3 velocity, bool standingOnPlatform = false)
	{
		UpdateRaycastOrigins();
		collisionInfo.Reset();
		collisionInfo.velocityOld = velocity;

		if (velocity.x != 0)
			HorizontalCollisions (ref velocity);
		//if (velocity.y != 0)
			VerticalCollisions (ref velocity);

		if (velocity.y > 0)
			collisionInfo.movingUp = true;
		transform.Translate(velocity);
	}
	
	void VerticalCollisions (ref Vector3 velocity)
	{
		float directionY = Mathf.Sign(velocity.y);
		float rayLength = Mathf.Abs(velocity.y) + GetSkinWidth();
		
		for (var i = 0; i < verticalRayCount; i++)
		{
			Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
			rayOrigin += Vector2.right * (GetVerticalRaySpacing() * i + velocity.x);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);
			
			//Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

			if (hit)
			{
				velocity.y = (hit.distance - GetSkinWidth()) * directionY;
				rayLength = hit.distance;

				collisionInfo.above = directionY == 1;
				collisionInfo.below = directionY == -1;
			}
		}
	}

	void HorizontalCollisions (ref Vector3 velocity)
	{
		//Which direction horizontally are we moving
		float directionX = Mathf.Sign(velocity.x);
		float rayLength = Mathf.Abs(velocity.x) + GetSkinWidth();

		//For each horizontal ray
		for (var i = 0; i < horizontalRayCount; i++)
		{
			//Cast the ray
			Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
			rayOrigin += Vector2.up * (GetHorizontalRaySpacing() * i);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

			//Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

			if (hit)
			{
				//Don't move through things
				velocity.x = (hit.distance - GetSkinWidth()) * directionX;
				rayLength = hit.distance;

				collisionInfo.left = directionX == -1;
				collisionInfo.right = directionX == 1;
			}
		}
	}
}