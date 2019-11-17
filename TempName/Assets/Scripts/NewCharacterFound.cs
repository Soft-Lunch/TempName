using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCharacterFound : MonoBehaviour
{

    public bool rockyUnlocked = true;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("Player found new character");

            // Todo interaction with new character
            if (rockyUnlocked)
                SpongeBehavior.rockyUnlocked = true;
            else
                SpongeBehavior.liamUnlocked = true;
        }
    }
}