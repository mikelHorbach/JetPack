using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {

    public float jetpackForce = 75.0f;
    public float forwardSpeed = 3.0f;
    private Rigidbody2D body;
    // Use this for initialization
    void Start () {
        body = this.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        bool jetpackActive = Input.GetButton("Fire1");
        
        if (jetpackActive)
        {   
            body.AddForce(new Vector2(0, jetpackForce)); 
        }
        Vector2 newVelocity = body.velocity;
        newVelocity.x = forwardSpeed;
        body.velocity = newVelocity;
    }
}
