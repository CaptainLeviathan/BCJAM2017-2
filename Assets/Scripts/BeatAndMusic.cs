using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatAndMusic : MonoBehaviour {

    public float BPM = 120f;
    float startOffSet = 0f;
    float beatDuration;
    float beatTimer;

	// Use this for initialization
	void Start ()
    {
        beatDuration = 1 / (BPM / 60);
	}
	
	// Update is called once per frame
	void Update ()
    {
        beatTimer += Time.deltaTime;
        
        if(beatTimer >= beatDuration)
        {
            beatTimer = 0f;
            BroadcastMessage("Beat");
        }	
	}
}
