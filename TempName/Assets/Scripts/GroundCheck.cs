using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private SpongeBehavior sponge;

    private void Awake()
    {
        if (transform.parent != null)
            sponge = transform.parent.GetComponent<SpongeBehavior>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        sponge.groundCheck = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        sponge.groundCheck = false;
    }

}
