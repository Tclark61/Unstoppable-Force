using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour {

    [SerializeField]
    private float range;
    [SerializeField]
    private float rangeLimit;
    [SerializeField]
    private float duration;


    private bool isGoingUp;

	// Use this for initialization
	void Start ()
    {
        range = 0.0f;
        isGoingUp = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isGoingUp)
        {
            this.transform.position = new Vector3(0.0f, Mathf.Sin(range) + transform.position.y, 0.0f);
            range += 0.1f/duration;
            if (range >= rangeLimit)
                isGoingUp = false;
        }
        else
        {
            this.transform.position = new Vector3(0.0f, Mathf.Sin(range), 0.0f);
            range -= 0.1f / duration;

            if (range <= 0)
                isGoingUp = true;
        }
	}
}
