using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    private Vector2 direction;
    private Animator animacao;
    Rigidbody2D rb;
    public float jumpForce;
    bool isgrounded = false;
    public float horizontalSpeed;
    bool querPular = false;
    bool apertandoAndando;
    bool apertandoPular;
    bool isGoingUp = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animacao = GetComponent<Animator>();
    }
    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (direction.x != 0 || direction.y != 0){
            apertandoAndando = true;
            animacao.SetInteger("transition", 1);

            if (direction.x > 0){
                transform.eulerAngles = new Vector2(0, 0);
            }
            else if (direction.x < 0){
                transform.eulerAngles = new Vector2(0, 180);
            }
        }
        else {
            apertandoAndando =false;
        }

        if (((Input.GetButton("Jump"))&& isgrounded)){ 
            Debug.Log("pulando");
            animacao.SetInteger("transition", 2);
            querPular = true;
            apertandoPular = true;
         }

        else{
            apertandoPular =false;
        }
         
        if (isGoingUp)
        {
            animacao.SetInteger("transition", 2);
        }

        if(apertandoPular || apertandoAndando || isGoingUp){
         
         //  animacao.SetInteger("transition", 0);
        }
        else
        {
            animacao.SetInteger("transition", 0);
            Debug.Log("idle");
        }
    }

    private void FixedUpdate(){

        rb.velocity = new Vector2(direction.x * horizontalSpeed, rb.velocity.y);
        if (querPular)
        {
            //rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            querPular = false;
            isGoingUp = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.tag == "PISO")
        {
            isgrounded = true;
            isGoingUp = false;
        }
    }


    private void OnTriggerExit2D(Collider2D collision){
        if (collision.tag == "PISO")
        {
            isgrounded = false;
        }
    }
}
