using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScripts : MonoBehaviour {
    public Sprite laserOnSprite;
    public Sprite laserOffSprite;
   
    public float switch_Interval = 0.5f;
    public float rotationSpeed = 0.0f;
  
    private bool isLaserOn = true;
    private float timeUntilNextSwitch;
    private Collider2D laserCollider;
    private SpriteRenderer laserRenderer;

    // Use this for initialization
    void Start () {
        timeUntilNextSwitch = switch_Interval;
        laserCollider = gameObject.GetComponent<Collider2D>();
        laserRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        timeUntilNextSwitch -= Time.deltaTime;
        if(timeUntilNextSwitch<= 0 )
        {
            isLaserOn = !isLaserOn;
            laserCollider.enabled = isLaserOn;
            if(isLaserOn)
            {
                laserRenderer.sprite = laserOnSprite;
            }
            else
            {
                laserRenderer.sprite = laserOffSprite;
            }
            timeUntilNextSwitch = switch_Interval;
        }
        transform.RotateAround(transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
    }
   
}
