using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundAI : MonoBehaviour
{

    [SerializeField]
    private float speed = 0;

    public float minX;
    public float maxX;

    [SerializeField]
    private bool moveRight = true;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (moveRight)
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
			if (transform.position.x > maxX) {
				Vector3 temp = transform.localScale;
				temp.x *= -1;
				transform.localScale = temp;
				moveRight = false;
			}
        }
        else
        {
            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
			if (transform.position.x < minX) {
				Vector3 temp = transform.localScale;
				temp.x *= -1;
				transform.localScale = temp;
				moveRight = true;
			}
        }
    }
}
