using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public float acceleration;
    public float angularAcceleration;
    public int rotationDegreesInterval;

    private Rigidbody2D rb;
    private float targetRotation;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Systems online!");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // ===========================
        // Update player acceleration
        // ===========================
        Vector2 force = (Vector2)transform.up * Input.GetAxis("Vertical") * acceleration;
        rb.AddForce(force);

        // ===========================
        // Update player's rotation                
        // ===========================

        // GetAxisRaw will return -1 or 1
        float horizontalAxis = Input.GetAxisRaw("Horizontal");

        // Get current player's rotation. We only care about the z-axis since this is a 2D game
        int z = Mathf.RoundToInt(transform.rotation.eulerAngles.z);
        
        // Check if player hit the horizontal input
        if (horizontalAxis > 0)
        {
            // horizontal axis > 0 means that the desired direction is *clockwise*
            // Note that unity's rotation goes from 0 to 360 in a counter-clockwise direction
            // Calculate the target rotation to be the closest interval behind the player's current rotation            
            if (z % rotationDegreesInterval == 0)
            {                
                targetRotation = z - rotationDegreesInterval;
            }
            else
            {
                targetRotation = z - (z % rotationDegreesInterval);
            }
        }
        else if (horizontalAxis < 0)
        {
            // horizontal axis < 0 means that the desired direction is *counter-clockwise*
            // Calculate the target rotation to be the closest interval ahead of player's current rotation
            // Debug.Log("Horizontal Axis < 0!!");
            // Debug.Log("[1]: " + z);
            // Debug.Log("[2]: " + (z % rotationDegreesInterval));
            // Debug.Log("[3]: " + (rotationDegreesInterval));
            targetRotation = z - (z % rotationDegreesInterval) + rotationDegreesInterval;
        }

        // Rotate our transform a step closer to the target rotation    
        // TODO: transform.Rotate(xAngle: 0f, yAngle: 0f, zAngle: amount);
        transform.rotation = Quaternion.RotateTowards(from: transform.rotation, to: Quaternion.Euler(0f, 0f, targetRotation), maxDegreesDelta: angularAcceleration * Time.deltaTime);
        // Debug.Log("Target rotation: " + targetRotation);
        // Debug.Log("Current rotation: " + z);
        // Debug.Log("z%: " + (z % rotationDegreesInterval));
    }

    public float GetTargetRotation()
    {
        return targetRotation;
    }
}
