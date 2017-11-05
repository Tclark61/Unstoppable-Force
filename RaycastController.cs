using UnityEngine;
using System.Collections;

public abstract class RaycastController : MonoBehaviour
{
	public struct RaycastOrigins
	{
		public Vector2 topLeft;
		public Vector2 topRight;
		public Vector2 bottomLeft;
		public Vector2 bottomRight;
	}

	public int horizontalRayCount = 4;
	public int verticalRayCount = 4;
	
	private float horizontalRaySpacing;
	private float verticalRaySpacing;
	private float skinWidth = .015f;

	private BoxCollider2D boxCollider;
	[HideInInspector]
	public RaycastOrigins raycastOrigins;

	public virtual void Start ()
	{
		boxCollider = GetComponent<BoxCollider2D>();
		CalculateRaySpacing();
	}

	void CalculateRaySpacing ()
	{
		Bounds bounds = boxCollider.bounds;
		bounds.Expand(skinWidth * -2);
		
		if (horizontalRayCount <= 1 || verticalRayCount <= 1)
		{
			Debug.Log("Ray count too low");
			return;
		}
		
		horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
		verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
	}
	
	public void UpdateRaycastOrigins ()
	{
		Bounds bounds = boxCollider.bounds;
		//Shrink bounds of skin so the rays can travel a little before colliding
		bounds.Expand(skinWidth * -2);
		
		raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
		raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
		raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
		raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
	}

	public float GetHorizontalRaySpacing ()
		{return horizontalRaySpacing;}
	public float GetVerticalRaySpacing ()
		{return verticalRaySpacing;}
	public float GetSkinWidth ()
		{return skinWidth;}
}
