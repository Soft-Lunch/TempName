using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class PresurePlate : MonoBehaviour
{
    public UnityEvent onPressed;
    public UnityEvent onRelease;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("DynamicObject"))
            onPressed.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("DynamicObject"))
            onRelease.Invoke();
    }
}
