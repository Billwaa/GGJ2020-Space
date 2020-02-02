﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{

    musicControl mc;
    AudioSource aud;

    public AudioClip death;

    bool restart = false;
    float deathTime = 0;


    // Start is called before the first frame update
    void Start()
    {
        mc = GameObject.FindObjectOfType<musicControl>();
        aud = this.GetComponent<AudioSource>();
        mc.filterThreshhold = 0;
        restart = false;
        deathTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (mc.filterThreshhold < 5)
            mc.filterThreshhold += 0.1f * Time.deltaTime;

        if (Input.GetKey(KeyCode.Alpha1))
            SceneManager.LoadScene(0);
        else if (Input.GetKey(KeyCode.Alpha2))
            SceneManager.LoadScene(1);
        else if (Input.GetKey(KeyCode.Alpha3))
            SceneManager.LoadScene(2);

        if (restart)
        {
            deathTime += Time.deltaTime;
            if(deathTime > 2)
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!restart & collision.gameObject.tag == "Hot")
        {
            aud.PlayOneShot(death);
            restart = true;
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }

        if (collision.gameObject.tag == "Finish")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }


}
