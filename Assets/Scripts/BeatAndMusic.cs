using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatAndMusic : MonoBehaviour
{

    public float BPM = 120f;
    public float fadeSpeed2D3D;

    [Range(0.0f, 1.0f)]
    public float voulum;

    public float beatStartOffSet = 0f;
    public float musicStartOffSet = 0f;
    float beatDuration;
    float beatTimer;
    float VolumBias = 1f;

    public AudioSource player2D;
    public AudioSource player3D;


    // Use this for initialization
    void Start()
    {
        beatDuration = 1 / (BPM / 60);
        player2D.volume = voulum;
        player3D.volume = 0;
        StartCoroutine(waiter());
        player2D.PlayDelayed(musicStartOffSet);
        player3D.PlayDelayed(musicStartOffSet);
    }

    // Update is called once per frame
    void Update()
    {
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

    IEnumerator waiter ()
    {
        yield return new WaitForSeconds(beatStartOffSet);
        StartCoroutine(beater());
    }

    IEnumerator beater()
    {
        while(true)
        {
            yield return new WaitForSeconds(beatDuration);
            Debug.Log("beat start");
            GameObject[] BeatListers = GameObject.FindGameObjectsWithTag("BeatListener");

            for (int i = 0; i < BeatListers.Length; ++i)
            {
                Debug.Log("beat sent:" + i);
                BeatListers[i].SendMessage("Beat", SendMessageOptions.DontRequireReceiver);
            }
        }
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

        player2D.volume = (VolumBias) * voulum;
        player3D.volume = (1 - VolumBias) * voulum;
    }

    public void mute()
    {
        voulum = 0;
    }
}
