using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoiseText : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("Perlin Noise Value" + Mathf.PerlinNoise(.5f, .1f));
        Debug.Log("Perlin Noise Value" + Mathf.PerlinNoise(.5f, .2f));
        Debug.Log("Perlin Noise Value" + Mathf.PerlinNoise(.5f, .3f));
        Debug.Log("Perlin Noise Value" + Mathf.PerlinNoise(.5f, .4f));
        Debug.Log("Perlin Noise Value" + Mathf.PerlinNoise(.5f, .5f));
    }
}
