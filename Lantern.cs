using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour {

    [SerializeField]
    private float lowerIntensityBound;

    [SerializeField]
    private float upperIntensityBound;

    private Light lt;
    private bool lightingUp;

    [SerializeField]
    private float duration;

    // Use this for initialization
    void Start ()
    {
        lt = GetComponent<Light>();
        lt.intensity = lowerIntensityBound;
        lightingUp = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(lightingUp)
        {
            lt.intensity += upperIntensityBound/duration;
            if (lt.intensity >= upperIntensityBound)
                lightingUp = false;
        }
        else
        {
            lt.intensity -= upperIntensityBound / duration;
            if (lt.intensity <= lowerIntensityBound)
                lightingUp = true;
        }
	}
}
