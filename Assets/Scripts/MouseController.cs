﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MouseController : MonoBehaviour {

    public static MouseController mouse;

    public float jetpackForce = 75.0f;
    public float forwardSpeed = 3.0f;
    private Rigidbody2D body;
    public Transform groundCheckTransform;
    private bool isGrounded;
    public LayerMask groundCheckLayerMask;
    private Animator mouseAnimator;
    public ParticleSystem jetpack;
    private bool isDead = false;
    private uint coins = 0;
    public Text coinsCollectedLabel;
    public Button restartButton;
    public AudioClip coinCollectSound;
    public AudioSource jetpackAudio;
    public AudioSource footstepsAudio;
    public ParallaxScroll parallax;
    public GameObject coinsTxt;

    private uint coef = 1;
    private bool isProtected = false;

    void Awake()
    {
        mouse = this;
    }

    // Use this for initialization
    void Start () {
        body = this.GetComponent<Rigidbody2D>();
        mouseAnimator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        bool jetpackActive = Input.GetButton("Fire1");
        jetpackActive = jetpackActive & !isDead;


        if (jetpackActive)
        {   
            body.AddForce(new Vector2(0, jetpackForce)); 
        }
        if (!isDead)
        {
            Vector2 newVelocity = body.velocity;
            newVelocity.x = forwardSpeed;
            body.velocity = newVelocity;
        }
        UpdateGroundedStatus();
        AdjustJetpack(jetpackActive);

        if (isDead && isGrounded)
        {
            restartButton.gameObject.SetActive(true);
        }
        AdjustFootstepsAndJetpackSound(jetpackActive);
        parallax.offset = transform.position.x;
    }

    void UpdateGroundedStatus()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.1f, groundCheckLayerMask);
        mouseAnimator.SetBool("isGrounded", isGrounded);
    }

    void AdjustJetpack(bool jetpackActive)
    {
        var jetpackEmission = jetpack.emission;
        jetpackEmission.enabled = !isGrounded;
        if (jetpackActive)
        {
            jetpackEmission.rateOverTime = 300.0f;
        }
        else
        {
            jetpackEmission.rateOverTime = 75.0f;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Coins"))
        {
            CollectCoin(collider);
        }
        else if (collider.gameObject.CompareTag("Laser"))
        {
            HitByLaser(collider);
        }

        else if (collider.gameObject.CompareTag("Pig"))
        {
            hitByPig(collider);
        }
        else if (collider.gameObject.CompareTag("Protection"))
        {
            hitByProtect(collider);
        }

    }

    void HitByLaser(Collider2D laserCollider)
    {
        if (!isProtected) { 
        if (!isDead)
        {
            AudioSource laserZap = laserCollider.gameObject.GetComponent<AudioSource>();
            laserZap.Play();
        }
        isDead = true;
        mouseAnimator.SetBool("isDie", true);
    }
    }

    public void Die()
    {
        isDead = true;
        mouseAnimator.SetBool("isDie", true);
    }

    void CollectCoin(Collider2D coinCollider)
    {
        coins+=(1*coef);
        coinsCollectedLabel.text = coins.ToString();
        Destroy(coinCollider.gameObject);
        AudioSource.PlayClipAtPoint(coinCollectSound, transform.position);
    }

    void hitByPig(Collider2D collider)
    {
       Destroy(collider.gameObject);
        coef = 2;
        incr();
    }

    void hitByProtect(Collider2D collider)
    {
        protect();
        Destroy(collider.gameObject);
    }
    void incr()
    {
        coinsTxt.SetActive(true);
       // print(coinsTxt.enabled);
        StartCoroutine(doubleMonets());
    }

    void slowTime()
    {

    }

    void protect()
    {
        isProtected = true;
        StartCoroutine(doProtect());
    }

    IEnumerator doubleMonets()
    {
        yield return new WaitForSeconds(15.0f);
        coef = 1;
        coinsTxt.SetActive(false);
    }

    IEnumerator doProtect()
    {
        yield return new WaitForSeconds(15.0f);
        isProtected = false;
    }

    IEnumerator slow()
    {
        yield return new WaitForSeconds(15.0f);
        coef = 1;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("RocketMouse.unity");
    }

    void AdjustFootstepsAndJetpackSound(bool jetpackActive)
    {
        footstepsAudio.enabled = !isDead && isGrounded;
        jetpackAudio.enabled = !isDead && !isGrounded;
        if (jetpackActive)
        {
            jetpackAudio.volume = 1.0f;
        }
        else
        {
            jetpackAudio.volume = 0.5f;
        }
    }

    public void pause()
    {
        if (Time.timeScale == 0) Time.timeScale = 1;
        else Time.timeScale = 0;
    }

    
}
