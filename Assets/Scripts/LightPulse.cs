using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPulse : MonoBehaviour {

    public float fadeSpeed;
    public float fadeDownTo;
    public Light Light;

    float initIntesity;

    void Start()
    {
        Light = GetComponent<Light>();
        initIntesity = Light.intensity;
    }
	
	// Update is called once per frame
	void Update ()
    {
         
        if (Light.intensity >= fadeDownTo)
        {
            Light.intensity -= fadeSpeed * Time.deltaTime;
        }
    }

    void Beat()
    {
        Light.intensity = initIntesity;
    }
}
