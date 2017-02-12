using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatAndMusic : MonoBehaviour
{

    public float BPM = 120f;
    float startOffSet = 0f;
    float beatDuration;
    float beatTimer;

    // Use this for initialization
    void Start()
    {
        beatDuration = 1 / (BPM / 60);
    }

    // Update is called once per frame
    void Update()
    {
        beatTimer += Time.deltaTime;

        if (beatTimer >= beatDuration)
        {
            Debug.Log("beat start");
            GameObject[] BeatListers = GameObject.FindGameObjectsWithTag("BeatListener");

            for (int i = 0; i < BeatListers.Length; ++i)
            {
                Debug.Log("beat sent:" + i);
                BeatListers[i].SendMessage("Beat", SendMessageOptions.DontRequireReceiver);
            }

            beatTimer = 0f;
        }

        if(Dimension.is2D())
        {

        }else if(Dimension.is3D())
        {

        }
    }
}
