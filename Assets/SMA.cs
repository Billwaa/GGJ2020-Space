using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMA : MonoBehaviour
{
    bool expand = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!expand)
            if (transform.localScale.x > 1)
                transform.localScale -= new Vector3(8, 8, 8)*Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hot")
            if (!expand)
            {
                expand = true;
                this.transform.localScale = new Vector3(4, 4, 4);
            }

            

    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Hot")
            if (expand)
            {
                expand = false;
                //this.transform.localScale = new Vector3(1, 1, 1);
            }


    }

}
