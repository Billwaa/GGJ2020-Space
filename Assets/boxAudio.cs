using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxAudio : MonoBehaviour
{

    AudioSource au;
    public AudioClip impact;

    // Start is called before the first frame update
    void Start()
    {
        au = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        au.PlayOneShot(impact);
    }


}
