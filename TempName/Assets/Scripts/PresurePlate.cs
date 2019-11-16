using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class PresurePlate : MonoBehaviour
{
    public UnityEvent onPressed;
    public UnityEvent onRelease;

    public Sprite pressed;
    private Sprite released;
    private SpriteRenderer render;

    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        released = render.sprite;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("DynamicObject"))
        {
            onPressed.Invoke();
            render.sprite = pressed;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("DynamicObject"))
        {
            onRelease.Invoke();
            render.sprite = released;
        }
    }
}
