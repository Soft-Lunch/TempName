using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RockyBehavior : MonoBehaviour
{
    public float accel = .8f;
    public float maxSpeed = 10f;
    public float jumpForce = 2f;
    public float jumpImpulse = 10f;
    public float jumpTime = 0.3f;
    public float secondsStoppedJumping = .5f;

    public float gravity = 1f;

    public Animator animator;
    public Transform GPX;

    public RuntimeAnimatorController rockyController;

    public ParticleSystem puff;

    private Vector2 spawnPos;

    private Vector2 move;
    private Rigidbody2D rb;
    private BoxCollider2D box;

    public Image selectedImage;
    public Image image;

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

        if (SpongeBehavior.rockyUnlocked)
            image.gameObject.SetActive(true);
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

                move.y = 0;


                if (gamePad.buttonWest.wasPressedThisFrame)
                {
                    if (SpongeBehavior.liamUnlocked)
                    {
                        puff.Play();

                        LiamBehavior liam = GetComponent<LiamBehavior>();
                        liam.enabled = true;

                        this.enabled = false;
                    }
                }
                else if (gamePad.buttonNorth.wasPressedThisFrame)
                {
                    puff.Play();

                    SpongeBehavior sponge = GetComponent<SpongeBehavior>();
                    sponge.enabled = true;

                    this.enabled = false;
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

                move.y = 0;

                if (keyboard.digit1Key.wasPressedThisFrame)
                {
                    if (SpongeBehavior.liamUnlocked)
                    {
                        puff.Play();

                        LiamBehavior liam = GetComponentInParent<LiamBehavior>();
                        liam.enabled = true;

                        this.enabled = false;
                    }
                }
                else if (keyboard.digit2Key.wasPressedThisFrame)
                {
                    puff.Play();

                    SpongeBehavior rocky = GetComponentInParent<SpongeBehavior>();
                    rocky.enabled = true;

                    this.enabled = false;
                }


                if (keyboard.spaceKey.isPressed)
                    jump = true;
                else
                    jump = false;
            }

            if (!ceilCheck)
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
        if (stop)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if (move != Vector2.zero)
        {
            rb.AddForce(move * accel * Time.fixedDeltaTime * 100);
        }

        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }

        if (rb.velocity.normalized.x > 0 && move.x < 0 ||
            rb.velocity.normalized.x < 0 && move.x > 0 ||
            move == Vector2.zero)

            rb.velocity = new Vector2(0, rb.velocity.y);

        if (jump && !ceilCheck)
        {
            if (groundCheck && !dontJump)
            {
                animator.SetBool("Jump", jump);
                StartCoroutine(WaitToJump());
                dontJump = true;
            }
        }

        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        if (rb.velocity.y > 0 && !groundCheck)
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
        if (!enabled)
            return;
        else if (collision.transform.parent && collision.transform.parent.gameObject != gameObject && collision.gameObject.CompareTag("Die"))
        {
            dead = true;
            startDeath = true;
            Debug.Log("Die");
        }
        else if (collision.gameObject.CompareTag("DynamicObject"))
            collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnEnable()
    {
        rb.gravityScale = gravity;
        animator.runtimeAnimatorController = rockyController;
        selectedImage.gameObject.SetActive(true);
        image.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        if (selectedImage)
            selectedImage.gameObject.SetActive(false);
        if (image)
            image.gameObject.SetActive(true);
    }
}
