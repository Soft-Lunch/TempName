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
        sponge.ceilCheck = true;
        rocky.ceilCheck = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        sponge.ceilCheck = false;
        rocky.ceilCheck = false;
    }
}
