using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{

    private float lowerFloatingBound;

    private float upperFloatingBound;

    [SerializeField]
    private float additive;

    private bool movingUp;

    [SerializeField]
    private float range;


    // Use this for initialization
    void Start()
    {
        movingUp = true;
        upperFloatingBound = transform.position.y + range;
        lowerFloatingBound = transform.position.y - range;
    }

    // Update is called once per frame
    void Update()
    {

        if (movingUp)
        {
            transform.position += new Vector3(0, additive, 0);
            if (transform.position.y >= upperFloatingBound)
                movingUp = false;
        }
        else
        {
            transform.position -= new Vector3(0, additive, 0);
            if (transform.position.y <= lowerFloatingBound)
                movingUp = true;
        }
    }
}
