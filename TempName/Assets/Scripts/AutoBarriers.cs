using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoBarriers : MonoBehaviour
{
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

            if (nextUp)
            {
                float posY = transform.position.y + (speed * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, posY, transform.position.z);
            }
            else
            {
                float posY = transform.position.y - (speed * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, posY, transform.position.z);
            }
        }

        timeCont += Time.deltaTime;
    }
}
