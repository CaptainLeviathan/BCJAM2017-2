using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalPulse : MonoBehaviour {

    public float fadeSpeed;
    public float emissionRateDownTo;

    float initEmisionRate;
    float fEmissionRate;

    ParticleSystem partSys;

    void Start ()
    {
        partSys = GetComponent<ParticleSystem>();
        initEmisionRate = partSys.emissionRate;
    }

    void Update()
    {
        if (partSys.emissionRate >= emissionRateDownTo)
        {
            partSys.emissionRate -= fadeSpeed * Time.deltaTime;
        }
    }

    void Beat()
    {
        partSys.emissionRate = initEmisionRate;
    }
}
