using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
public class MouseOrbit : MonoBehaviour {
 
    public Transform target;
    public float distance = 5.0f;
    public float xSpeed = 60.0f;
    public float ySpeed = 60.0f;
 
    public float yMinLimit = -10f;
    public float yMaxLimit = 25f;
 
    public float distanceMin = .5f;
    public float distanceMax = 7.5f;

    public float yOffset;
    
    private Rigidbody rb;
 
    float x = 0.0f;
    float y = 0.0f;
 
    // Use this for initialization
    void Start () 
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
 
        rb = GetComponent<Rigidbody>();
 
        // Make the rigid body not change rotation
        if (rb != null)
        {
            rb.freezeRotation = true;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
 
    void LateUpdate ()
    {
        if (target && !PlayerManager.isPaused) 
        {
            x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
 
            y = ClampAngle(y, yMinLimit, yMaxLimit);
 
            Quaternion rotation = Quaternion.Euler(y, x, 0);
 
            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel")*5, distanceMin, distanceMax);
            
            //this Vector3 is the line used in the next else block
            Vector3 playerToCamVect = (transform.position - target.position).normalized * distanceMax;

            RaycastHit hit;
            /*if (Physics.Linecast (target.position, transform.position, out hit)){
            
                //something is in between where the camera wants to be and the player.
                //simply move in the camera so it's 75% of the way to whatever horrible
                //thing is trying to get between it and the player
                //distance = hit.distance * 0.75f;
            }else if(Physics.Linecast (target.position, target.position + playerToCamVect, out hit)){
                //okay so this time we extend the line from player to camera an extra length
                //(to be precise, it extends distanceMax length) and use that to find if the
                //camera could be further out without clipping into something else.
                distance = hit.distance * 0.8f;
            }*/

            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;
 
            transform.rotation = rotation;
            transform.position = new Vector3(position.x, position.y+yOffset,position.z);
        }
    }
 
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}