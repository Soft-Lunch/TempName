using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpongeBehavior : MonoBehaviour
{
    public float accel = .8f;
    public float maxSpeed = 10f;
    public float crouchSpeedFactor = .8f;

    private Vector2 spawnPos;

    private Vector2 move;
    private Rigidbody2D rb;
    private BoxCollider2D box;

    private bool crouch = false;

    [HideInInspector]
    public bool ceilCheck = false;

    //Death
    //-----------------------------------
    private bool dead = false;
    private bool startDeath = false;

    private float deathTimer = 0f;
    public float deathTime = 2f; // From inspector
    //-----------------------------------

    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();

        animator = GetComponent<Animator>();

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

                if (move.y < 0)
                    crouch = true;
                else if (move.y >= 0)
                    crouch = false;

                move.y = 0;
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
            }

            if (move == Vector2.zero)
                rb.velocity = Vector2.zero;

            if (rb.velocity.normalized.x > 0 && move.x < 0 ||
                rb.velocity.normalized.x < 0 && move.x > 0 ||
                move == Vector2.zero)

                rb.velocity = Vector2.zero;

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
            }
        }             
    }         

    private void FixedUpdate()
    {
        if (move != Vector2.zero)
        {
            rb.AddForce(move * accel * Time.fixedDeltaTime * 100);         
        }     

        if (crouch)
        {
            if (rb.velocity.magnitude > maxSpeed * crouchSpeedFactor)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed * crouchSpeedFactor;
            }
        }
        else
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Die"))
        {
            dead = true;
            startDeath = true;
        }
    }
}
