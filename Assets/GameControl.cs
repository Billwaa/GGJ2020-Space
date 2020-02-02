using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{

    musicControl mc;



    // Start is called before the first frame update
    void Start()
    {
        mc = GameObject.FindObjectOfType<musicControl>();
        mc.filterThreshhold = 0;
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

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Hot")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (collision.gameObject.tag == "Finish")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }


}
