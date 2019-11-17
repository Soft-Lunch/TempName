using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private SpongeBehavior sponge;
    private RockyBehavior rocky;

    private void Awake()
    {
        if (transform.parent != null)
        {
            sponge = transform.parent.GetComponent<SpongeBehavior>();
            rocky = transform.parent.GetComponent<RockyBehavior>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("DynamicObject"))
        {
            sponge.groundCheck = true;
            rocky.groundCheck = true;
        }
    }

    private void OnTriggerContinue2D(Collider2D collision)
    {
        if (!collision.CompareTag("DynamicObject"))
        {
            sponge.groundCheck = true;
            rocky.groundCheck = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("DynamicObject"))
        {
            sponge.groundCheck = false;
            rocky.groundCheck = false;
        }

    }
}
