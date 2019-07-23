using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public float acceleration;
    // public float angularAcceleration;
    // public int rotationDegreesInterval;
    // public Transform forcePosition;    

    public TestJoint leftEngine;
    public TestJoint rightEngine;

    public GameObject frontLeftEngineSprite;
    public GameObject frontRightEngineSprite;
    public GameObject rearLeftEngineSprite;
    public GameObject rearRightEngineSprite;    

    // private Rigidbody2D rb;
    private float targetRotation;
    // private float direction;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Systems online!");        
        // rb.AddForceAtPosition(force: transform.up * acceleration, position: forcePosition.position);
    }

    private void Update()
    {
        // *******************
        // rear left engine
        // *******************
        if (Input.GetKey(KeyCode.A)) {
            Debug.Log("A");
            leftEngine.Accelerate(acceleration);            
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            rearLeftEngineSprite.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            rearLeftEngineSprite.SetActive(false);
        }

        // *******************
        // rear right engine
        // *******************
        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("D");
            rightEngine.Accelerate(acceleration);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            rearRightEngineSprite.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            rearRightEngineSprite.SetActive(false);
        }

        // *******************
        // front left engine
        // *******************
        if (Input.GetKey(KeyCode.Q))
        {
            Debug.Log("Q");
            leftEngine.Accelerate(-1 * acceleration);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            frontLeftEngineSprite.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            frontLeftEngineSprite.SetActive(false);
        }

        // *******************
        // front right engine
        // *******************
        if (Input.GetKey(KeyCode.E))
        {
            Debug.Log("E");
            rightEngine.Accelerate(-1 * acceleration);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            frontRightEngineSprite.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            frontRightEngineSprite.SetActive(false);
        }
    }

    /*
    private void Update()
    {
        int horizontal = (int)Input.GetAxisRaw("Horizontal");
        if (horizontal > 0)
        {
            direction = -1;
        }
        else if (horizontal < 0)
        {
            direction = 1;
        }

        if (Input.GetButtonDown("Jump"))
        {
            direction = 0;
        }
    }

    private void FixedUpdate()
    {        
        transform.Rotate(xAngle: 0f, yAngle: 0f, zAngle: angularAcceleration * direction);
        rb.velocity = transform.up * acceleration;
    }
    */
    /*
    // Update is called once per frame    
    void FixedUpdate()
    {        
        // ===========================
        // Update player's rotation                
        // ===========================
        // GetAxisRaw will return either -1 or 1. GetAxis will return a float from -1 to 1.
       

        float horizontalAxis = Input.GetAxis("Horizontal");
        transform.Rotate(xAngle: 0f, yAngle: 0f, zAngle: -horizontalAxis * angularAcceleration);

        // ===========================
        // Update player acceleration
        // ===========================
        Vector2 force = (Vector2)transform.up * Input.GetAxis("Vertical") * acceleration;
        // Vector2 force = (Vector2)transform.up * acceleration * Time.deltaTime;
        // rb.AddForce(force);
        rb.velocity = force;

        /*
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
        transform.rotation = Quaternion.RotateTowards(from: transform.rotation, to: Quaternion.Euler(0f, 0f, targetRotation), maxDegreesDelta: angularAcceleration * Time.deltaTime);
        // Debug.Log("Target rotation: " + targetRotation);
        // Debug.Log("Current rotation: " + z);
        // Debug.Log("z%: " + (z % rotationDegreesInterval));     
        */
    //}
    
    public float GetTargetRotation()
    {
        return targetRotation;
    }
}