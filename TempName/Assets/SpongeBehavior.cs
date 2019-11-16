using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpongeBehavior : MonoBehaviour
{
    public float accel = 10f;
    public float maxSpeed = 20f;

    private Vector2 move;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var gamePad = Gamepad.current;
        move = Vector2.zero;

        if (gamePad != null)
        {
            //Controls
            move = gamePad.leftStick.ReadValue();
            move.y = 0;
        }
        else
        {

            //Some keyboard support
            var keyboard = Keyboard.current;       

            if(keyboard.dKey.isPressed)
            {
                move.x += 1;
            }
            if (keyboard.aKey.isPressed)
            {
                move.x -= 1;
            }
        }
    }

    private void FixedUpdate()
    {
        if(move != Vector2.zero)
        {
            rb.AddForce(move * accel * Time.fixedDeltaTime * 100);         
        }

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}
