using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ninjaBehavor : MonoBehaviour {

    public int speed = 1;

    private Rigidbody2D body;
    private Animator animator;

    // Use this for initialization
    void Start () {
        body = this.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
