using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour {

    public int speed = 5;
    public float MaxJumpTime = 1f;
    public float JumpSpeed = 2f;

    private Rigidbody2D body;
    private Animator animator;
    private Mode mode;
    private bool canAttack;

    private bool JumpActive = true;
    private float JumpTime = 0f;

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
        if (mode == Mode.Stay)
        {
            Vector2 vel = body.velocity;
            vel.x = 0;
            body.velocity = vel;
            animator.SetBool("isWin", true);
        }

        if (mode == Mode.Jump&JumpActive)
        {
            animator.SetTrigger("isJumpAttack");
            this.JumpTime += Time.deltaTime;
            if (this.JumpTime < this.MaxJumpTime)
            {
                Vector2 vel = body.velocity;
                vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
                body.velocity = vel;
                if (GetComponent<BoxCollider2D>().IsTouching(MouseController.mouse.GetComponent<BoxCollider2D>()))
                {
                    MouseController.mouse.Die();
                }
            }
            else
            {
                this.JumpActive = false;
                this.JumpTime = 0;
                animator.SetBool("isWalk",true);
            }
        }

        checkHeroPosition();
    }

    void checkHeroPosition()
    {
        Vector3 mousePosition = MouseController.mouse.transform.position;
        Vector3 myPosition = this.transform.position;
        if (mousePosition.x + 5 > myPosition.x)
        {
            if (mousePosition.x < myPosition.x && mousePosition.x + 3 > myPosition.x && mousePosition.y - myPosition.y < 1)//GetComponent<BoxCollider2D>().IsTouching(MouseController.mouse.GetComponent<BoxCollider2D>())
            {
                attack();
                return;
            }
            if (mousePosition.x < myPosition.x && mousePosition.y - 3 > myPosition.y)
            {
                mode = Mode.Jump;
                canAttack = false;
            }
        }
        
    }


    void attack()
    {
        if (canAttack)
        {
            if (!animator.GetBool("isFloorAttack")) animator.SetTrigger("isFloorAttack");
            MouseController.mouse.Die();
            canAttack = false;
            mode = Mode.Stay;
        }

    }

    bool isGrounded()
    {
        Vector3 from = transform.position + Vector3.up * 0.3f;
        Vector3 to = transform.position + Vector3.down * 0.1f;
        int layer_id = 1 << LayerMask.NameToLayer("Ground");
        //Перевіряємо чи проходить лінія через Collider з шаром Ground
        RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
        Debug.Log(hit);
        if (hit) return true;
        return false;
    }

    public enum Mode
    {
        Run,
        Jump,
        Stay
    }
}
