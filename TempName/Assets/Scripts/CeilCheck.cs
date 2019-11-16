using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilCheck : MonoBehaviour
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
        if (sponge.enabled)
            sponge.ceilCheck = true;
        else if (rocky.enabled)
            rocky.ceilCheck = true;
    }
    private void OnTriggerContinue2D(Collider2D collision)
    {
        if (sponge.enabled)
            sponge.ceilCheck = true;
        else if (rocky.enabled)
            rocky.ceilCheck = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (sponge.enabled)
            sponge.ceilCheck = false;
        else if (rocky.enabled)
            rocky.ceilCheck = false;
    }

}
