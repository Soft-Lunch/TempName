using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilarEvent : MonoBehaviour
{
    bool goingUp = false;
    bool goingDown = false;

    public float velocity = 2;

    float posMaxY;
    float posMinY;
    public void Start()
    {
        posMaxY = transform.position.y;
        posMinY = transform.position.y - (GetComponent<BoxCollider2D>().size.y * transform.lossyScale.y);
    }
    // Update is called once per frame
    void Update()
    {
        if (goingDown)
        {
            float posY = transform.position.y - (velocity * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, posY, transform.position.z);
            if (transform.position.y <= posMinY)
            {
                transform.position = new Vector3(transform.position.x, posMinY, transform.position.z);
                goingDown = false;
            }

        }
        else if (goingUp)
        {
            float posY = transform.position.y + (velocity * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, posY, transform.position.z);
            if (transform.position.y >= posMaxY)
            {
                transform.position = new Vector3(transform.position.x, posMaxY, transform.position.z);
                goingUp = false;
            }
        }
    }


    public void PilarDown()
    {
        goingDown = true;
        goingUp = false;
    }
    public void PilarUp()
    {
        goingDown = false;
        goingUp = true;
    }
}
