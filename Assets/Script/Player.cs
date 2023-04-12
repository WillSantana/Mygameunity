using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    public float JumpForce;

    public bool isJumping;
    public bool doubleJump;

    private Rigidbody2D rig;
    private Animator anim;

    bool isBlowing;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    // Metodo de movimentação horizontal
    void Move()
    {
        float movement = Input.GetAxis("Horizontal");

        rig.velocity = new Vector2(movement * Speed, rig.velocity.y);
        
        if(movement > 0f)
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f,0f,0f);
        }
        else if(movement < 0f)
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f,180f,0f);  
        }
        else
        {
            anim.SetBool("walk", false);
        }
    }

    //Metodo de pulo
    void Jump()
    {
        if(Input.GetButtonDown("Jump") && !isBlowing)
        {
            if(!isJumping)
            {
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                doubleJump = true;
                anim.SetBool("jump", true);
            }
            else
            {
                if(doubleJump)
                {
                    rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                    doubleJump = false;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            isJumping = false;
            anim.SetBool("jump", false);
        }

        if(collision.gameObject.CompareTag("Spike") || collision.gameObject.CompareTag("saw") || collision.gameObject.CompareTag("ghost"))
        {
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            isJumping = true;
        }

        if(collision.gameObject.layer == 11)
        {
            isBlowing = false;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 11)
        {
            isBlowing = true;
        }
    }
}
