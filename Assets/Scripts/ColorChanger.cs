using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public Material material;
    [Range(0.0f, 1.0f)]
    public float maxR = 1f;
    [Range(0.0f, 1.0f)]
    public float maxG = 1f;
    [Range(0.0f, 1.0f)]
    public float maxB = 1f;
    [Range(0.0f, 1.0f)]
    public float minR = 0f;
    [Range(0.0f, 1.0f)]
    public float minG = 0f;
    [Range(0.0f, 1.0f)]
    public float minB = 0f;

    void Beat()
    {
        Debug.Log("beat");
        material.SetColor("_Color", new Color(Random.Range(minR, maxR), Random.Range(minG, maxG), Random.Range(minB, maxB)));
    }

}
