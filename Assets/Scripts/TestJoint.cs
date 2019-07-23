using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestJoint : MonoBehaviour
{
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Joint forces online!");
        rb = GetComponent<Rigidbody2D>();
        // rb.AddForce(transform.up * 20f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Accelerate(float acceleration)
    {
        rb.AddForce(transform.up * acceleration);
    }
}
