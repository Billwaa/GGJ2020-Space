using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGun : MonoBehaviour
{
    public Camera viewCam;
    public Transform hitVisual;
    public float maxRange = 50;
    public float grabRange = 20;
    public float dragForce = 20;
    public Rigidbody grabbedRigidbody;
    public float grabHoldDistance = 3;
    public float grabSpeed = 10;
    public float shootRange = 6;
    public float shootForce = 20;
    public float cooldownTimer = 0;
    public float cooldownTime = 0.5f;

    //public BlasterShot shot;

    void Update()
    {
        /*if (Input.GetMouseButtonDown(2))
        {
            Instantiate(shot, viewCam.transform.position + viewCam.transform.forward * 2, viewCam.transform.rotation);
        }*/

        RaycastHit hit;
        if (Input.GetMouseButton(1) && cooldownTimer <= 0)
        {
            if (grabbedRigidbody != null)
            {
                var rb = grabbedRigidbody;
                grabbedRigidbody = rb;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true;

                var grabPosition = viewCam.transform.position + viewCam.transform.forward * grabHoldDistance;
                var grabDirection = (grabPosition - rb.position).normalized;
                var grabTravelDistance = grabDirection * grabSpeed * Time.deltaTime;
                if (grabTravelDistance.sqrMagnitude < (grabPosition - rb.position).sqrMagnitude)
                {
                    rb.MovePosition(rb.position + grabTravelDistance);
                }
                else
                {
                    rb.MovePosition(grabPosition);
                }
            }
            else if (Physics.Raycast(viewCam.transform.position, viewCam.transform.forward, out hit, maxRange))
            {
                //Visualize Raycast
                Debug.DrawLine(viewCam.transform.position, hit.point, Color.red);
                if (hitVisual != null) hitVisual.position = hit.point;

                //Get rigidbody
                var rb = hit.collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    if (hit.distance < grabRange)
                    {
                        grabbedRigidbody = rb;
                        rb.angularVelocity = Vector3.zero;
                        rb.isKinematic = true;

                        /*
						var grabPosition = viewCam.transform.position + viewCam.transform.forward * grabHoldDistance;
						var grabDirection = (grabPosition - rb.position).normalized;
						var grabTravelDistance = grabDirection * grabSpeed * Time.deltaTime;
						if(grabTravelDistance.sqrMagnitude < (grabPosition - rb.position).sqrMagnitude) {
							rb.MovePosition( rb.position + grabTravelDistance );
						}
						else {
							rb.MovePosition( grabPosition );
						}
						*/
                    }
                    else
                    {
                        rb.AddForceAtPosition(viewCam.transform.forward * -dragForce, hit.point);
                    }
                }
            }
        }
        else
        {
            if (grabbedRigidbody != null)
            {
                grabbedRigidbody.isKinematic = false;
                grabbedRigidbody = null;
            }
        }

        RaycastHit shootHit;
        if (Input.GetMouseButtonDown(0) && cooldownTimer <= 0)
        {
            if (Physics.Raycast(viewCam.transform.position, viewCam.transform.forward, out shootHit, shootRange))
            {
                //Get rigidbody
                var rb = shootHit.collider.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    grabbedRigidbody.isKinematic = false;
                    grabbedRigidbody = null;

                    rb.AddForceAtPosition(viewCam.transform.forward * shootForce, shootHit.point, ForceMode.Impulse);

                    cooldownTimer = cooldownTime;
                }

            }
        }
        Debug.DrawLine(viewCam.transform.position, viewCam.transform.position + viewCam.transform.forward * grabHoldDistance, Color.cyan);

        if (cooldownTimer > 0) cooldownTimer -= Time.deltaTime;
    }
}