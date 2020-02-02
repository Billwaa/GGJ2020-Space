using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class musicControl : MonoBehaviour

   
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public float filterThreshhold = 0.5f;
    public AudioMixer filterMixer;
    public AudioMixerSnapshot[] filtersnapshots;
    public float[] weights;
    public float Intense = 0;
    

    public void BlendSnapShots (float Intensity)
    {
        weights[0] = Intensity;
        weights[1] = filterThreshhold - Intensity;

        filterMixer.TransitionToSnapshots(filtersnapshots, weights, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        BlendSnapShots(Intense);
    }
}
