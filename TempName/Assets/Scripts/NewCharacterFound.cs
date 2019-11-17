using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCharacterFound : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("Player found new character");

            // Todo interaction with new character

            
        }
    }
}