using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstableBox : MonoBehaviour
{
    public float timeStart = 2.0f;

    float cont = 0.0f;
    bool collisionContact = false;

    ParticleSystem particleSystem;
    BoxCollider2D box;
    Rigidbody2D rb;
    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        box = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (collisionContact && cont + timeStart <= Time.time)
            rb.isKinematic = false;
        if (particleSystem.isStopped && !box)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisionContact = true;
        cont = Time.time;
        if (collision.gameObject.CompareTag("Die"))
        {
            particleSystem.Play();
            box.enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            rb.isKinematic = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collisionContact = false;
    }
}
