using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {

    public float jetpackForce = 75.0f;

    // Use this for initialization
    void Start () {
		
	}

    void FixedUpdate()
    {
        bool jetpackActive = Input.GetButton("Fire1");

        if (jetpackActive)
        {
            Rigidbody2D  body = this.GetComponent<Rigidbody2D>();
            body.AddForce(new Vector2(0, jetpackForce));
        }
    }
}
