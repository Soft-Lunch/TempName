using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewCharacterFound : MonoBehaviour
{

    public bool rockyUnlocked = true;

    public GameObject rockyImage;
    public GameObject liamImage;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("Player found new character");

            // Todo interaction with new character
            if (rockyUnlocked)
            {
                SpongeBehavior.rockyUnlocked = true;
                rockyImage.SetActive(true);
            }
            else
            {
                liamImage.SetActive(true);
                SpongeBehavior.liamUnlocked = true;
            }
            GameObject.Destroy(gameObject.transform.parent.gameObject);
        }
    }
}