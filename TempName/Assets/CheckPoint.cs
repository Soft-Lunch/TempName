using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameObject cam;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SpongeBehavior.checkpoint = true;
            SpongeBehavior.spwanPos = transform.position;
            SpongeBehavior.cameraPos = cam.transform.position;

        }
    }
}