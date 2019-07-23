using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    public float wheeledAcceleration;
    [SerializeField]
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {                
    }

    void FixedUpdate()
    {
        Vector3 force = transform.right * Input.GetAxis("Horizontal") * wheeledAcceleration * Time.deltaTime;
        //Debug.Log("Force " + force);
        rb.velocity = force;        
    }
}
