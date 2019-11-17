using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoBarriers : MonoBehaviour
{
    public bool horizontal;
    public bool nextUp;
    public float speed;

    public float timeWaiting = 1.0f;
    public float timeMooving = 1.0f;

    float timeCont = 0.0f;
    bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        timeCont = 0.0f;   
    }

    // Update is called once per frame
    void Update()
    {
        if (timeCont >= timeWaiting && !isMoving)
        {
            isMoving = true;
            timeCont = 0.0f;
        }

        if (timeCont >= timeMooving && isMoving)
        {
            nextUp = !nextUp;
            isMoving = false;
            timeCont = 0.0f;
        }

        if (isMoving)
        {
            float pos = transform.position.y;
            if (horizontal)
                pos = transform.position.x;


            if (nextUp)
                pos += (speed * Time.deltaTime);
            else
                pos -= (speed * Time.deltaTime);


            if (!horizontal)
                transform.position = new Vector3(transform.position.x, pos, transform.position.z);
            else
                transform.position = new Vector3(pos, transform.position.y, transform.position.z);
        }

        timeCont += Time.deltaTime;
    }
}
