using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatAndMusic : MonoBehaviour
{

    public float BPM = 120f;
    public float fadeSpeed2D3D;

    float startOffSet = 0f;
    float beatDuration;
    float beatTimer;
    float VolumBias = 1f;

    public AudioSource player2D;
    public AudioSource player3D;


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
            VolumBias += fadeSpeed2D3D * Time.deltaTime;
        }
        else if(Dimension.is3D())
        {
            VolumBias -= fadeSpeed2D3D * Time.deltaTime;
        }

        fade();
    }

    void fade()
    {
        if (VolumBias > 1)
        {
            VolumBias = 1;
        }
        else if (VolumBias < 0)
        {
            VolumBias = 0;
        }

        player2D.volume = VolumBias;
        player3D.volume = 1 - VolumBias;
    }
}
