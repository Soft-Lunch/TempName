﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SpongeBehavior : MonoBehaviour
{
    public float accel = .8f;
    public float maxSpeed = 10f;
    public float crouchSpeedFactor = .8f;
    public float jumpForce = 2f;
    public float jumpImpulse = 10f;
    public float jumpTime = 0.3f;
    public float secondsStoppedJumping = .5f;

    public float gravity = 1f;

    public Animator animator;
    public Transform GPX;

    public ParticleSystem puff;

    public RuntimeAnimatorController spongeController;

    private Vector2 spawnPos;

    private Vector2 move;
    private Rigidbody2D rb;
    private BoxCollider2D box;

    private bool crouch = false;
    private bool jump = false;
    private bool dontJump = false;

    [HideInInspector]
    public bool ceilCheck = false;

    [HideInInspector]
    public bool groundCheck = false;

    [HideInInspector]
    public bool stop = false;

    //Death
    //-----------------------------------
    private bool dead = false;
    private bool startDeath = false;

    private float deathTimer = 0f;
    public float deathTime = 2f; // From inspector
    //-----------------------------------

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();

        spawnPos = transform.position;
    }

    private void Update()
    {
        move = Vector2.zero;

        if (!dead)
        {
            var gamePad = Gamepad.current;

            if (gamePad != null)
            {
                //Controls
                move = gamePad.leftStick.ReadValue();

                if (move.y < -0.5)
                    crouch = true;
                else
                    crouch = false;

                move.y = 0;
             
                if(!crouch && !ceilCheck)
                {
                    if (gamePad.buttonWest.wasPressedThisFrame)
                    {
                        //Blue player
                    }
                    else if (gamePad.buttonEast.wasPressedThisFrame)
                    {
                        puff.Play();

                        RockyBehavior rocky = GetComponentInParent<RockyBehavior>();
                        rocky.enabled = true;

                        this.enabled = false;
                    }
                }

                if (gamePad.buttonSouth.isPressed)
                    jump = true;
                else
                    jump = false;
            }
            else
            {
                //Some keyboard support
                var keyboard = Keyboard.current;

                if (keyboard.dKey.isPressed)
                {
                    move.x += 1;
                }

                if (keyboard.aKey.isPressed)
                {
                    move.x -= 1;
                }

                if (keyboard.sKey.isPressed)
                    crouch = true;
                else
                    crouch = false;
           
                move.y = 0;

                if (!crouch && !ceilCheck)
                {
                    if (keyboard.digit1Key.wasPressedThisFrame)
                    {
                        //Blue player
                    }
                    else if (keyboard.digit3Key.wasPressedThisFrame)
                    {
                        puff.Play();

                        RockyBehavior rocky = GetComponentInParent<RockyBehavior>();
                        rocky.enabled = true;

                        this.enabled = false;
                    }
                }

                if (keyboard.spaceKey.isPressed)
                    jump = true;
                else
                    jump = false;
            }

            if (crouch)
                box.enabled = false;

            else if (!ceilCheck)
                box.enabled = true; 
        }

        else if (startDeath)
        {
            startDeath = false;
            // Start death animation
            animator.SetBool("Death", true);
            deathTimer = 0f;

            // Puff
            puff.Play();
            rb.isKinematic = true;
        }

        else
        {
            deathTimer += Time.deltaTime;
            if (deathTimer >= deathTime)
            {
                // Restart current level
                animator.SetBool("Death", false);
                dead = false;
                deathTimer = 0;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        animator.SetBool("Crouch", crouch || ceilCheck);

        if (move.x > 0)
        {
            GPX.localScale = new Vector3(1, 1, 1);
        }
        else if (move.x < 0)
        {
            GPX.localScale = new Vector3(-1, 1, 1);
        }

        if (dead)
            box.enabled = false;
    }

    private void FixedUpdate()
    {
        if(stop)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if (move != Vector2.zero)
        {
            rb.AddForce(move * accel * Time.fixedDeltaTime * 100);
        }

        if (crouch || ceilCheck)
        {
            if (Mathf.Abs(rb.velocity.x) > maxSpeed * crouchSpeedFactor)
            {
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed * crouchSpeedFactor, rb.velocity.y);
            }
        }
        else if(Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }    
          
        if (rb.velocity.normalized.x > 0 && move.x < 0 ||
            rb.velocity.normalized.x < 0 && move.x > 0 ||
            move == Vector2.zero)

            rb.velocity = new Vector2(0, rb.velocity.y);

        if (jump && !crouch && !ceilCheck)
        {
            if (groundCheck && !dontJump)
            {
                animator.SetBool("Jump", jump);
                StartCoroutine(WaitToJump());
                dontJump = true;
            }               
        }
         
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));   
        
        if(rb.velocity.y > 0 && !groundCheck)
        {
            dontJump = false;
            animator.SetBool("Jump", false);
        }
    }

    private IEnumerator WaitToJump()
    {
        stop = true;
        yield return new WaitForSeconds(secondsStoppedJumping);
        stop = false;

        //Jump
        rb.AddForce(Vector2.up * jumpImpulse * 100 * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.parent.gameObject != gameObject && collision.gameObject.CompareTag("Die"))
        {
            dead = true;
            startDeath = true;
            Debug.Log("Die");
        }
    }

    private void OnEnable()
    {
        rb.gravityScale = gravity;
        animator.runtimeAnimatorController = spongeController;

    }
}
