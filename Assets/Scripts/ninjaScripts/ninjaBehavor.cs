using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ninjaBehavor : MonoBehaviour {

    public int speed = 1;

    private Rigidbody2D body;
    private Animator animator;
    private Mode mode;
    private bool canAttack;


    // Use this for initialization
    void Start () {
        body = this.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        mode = Mode.Run;
        canAttack = true;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (mode == Mode.Run)
        {
            Vector2 vel = body.velocity;
            vel.x = -speed;
            body.velocity = vel;
        }
        
        if(mode == Mode.Stay)
        {
            animator.SetBool("isRun", false);
            Vector2 vel = body.velocity;
            vel.x = 0;
            body.velocity = vel;
        }


        checkHeroPosition();
    }

    void checkHeroPosition()
    {
        Vector3 mousePosition = MouseController.mouse.transform.position;
        Vector3 myPosition = this.transform.position;
        if (mousePosition.x + 5 > myPosition.x) mode = Mode.Stay;
        if (mousePosition.x< myPosition.x && mousePosition.x + 2 > myPosition.x && mousePosition.y-myPosition.y<1)//GetComponent<BoxCollider2D>().IsTouching(MouseController.mouse.GetComponent<BoxCollider2D>())
        {
            attack();
        }
    }

    void attack()
    {
        if (canAttack)
        {
            if (!animator.GetBool("isAttack")) animator.SetTrigger("isAttack");
            MouseController.mouse.Die();
            canAttack = false;
        }
        
    }

    public enum Mode
    {
        Stay,
        Run,
        Fireball
    }
}
