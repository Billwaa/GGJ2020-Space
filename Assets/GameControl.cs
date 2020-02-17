using System.Collections;
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


    private void OnMouseEnter()
    {

        Cursor.lockState = CursorLockMode.Confined; // keep confined in the game window
        Cursor.lockState = CursorLockMode.Locked;   // keep confined to center of screen
        Cursor.lockState = CursorLockMode.None;     // set to default default
    }
   

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
        if(mc != null)
        if (mc.filterThreshhold < 5)
            mc.filterThreshhold += 0.5f * Time.deltaTime;

        if (Input.GetKey(KeyCode.Alpha1))
            SceneManager.LoadScene(1);
        else if (Input.GetKey(KeyCode.Alpha2))
            SceneManager.LoadScene(2);
        else if (Input.GetKey(KeyCode.Alpha3))
            SceneManager.LoadScene(3);

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
