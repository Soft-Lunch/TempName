using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilarEvent: MonoBehaviour
{
    bool goingUp = false;
    bool goingDown = false;

    Vector3 posMax;
    public void Start()
    {
        posMax = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if(goingDown)
        {
            transform.position.Set(transform.position.x, transform.position.y-1, transform.position.z);
            Debug.Log(" ");
        }
        else if(goingUp)
        {

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
