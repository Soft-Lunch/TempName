using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilarEvent : MonoBehaviour
{
    public bool freezePlayer = false;

    public SpongeBehavior sponge;
    public RockyBehavior rocky;
    public LiamBehavior liam;

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

                StartCoroutine(MovePlayer());

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

                StartCoroutine(MovePlayer());
            }
        }
    }

    private IEnumerator MovePlayer()
    {
        yield return new WaitForSeconds(1);

        if (freezePlayer)
        {
            sponge.stop = false;
            rocky.stop = false;
            liam.stop = false;
        }
    }



    public void PilarDown()
    {
        goingDown = true;
        goingUp = false;

        if (freezePlayer)
        {
            sponge.stop = true;
            rocky.stop = true;
            liam.stop = true;
        }
    }
    public void PilarUp()
    {
        goingDown = false;
        goingUp = true;

        if (freezePlayer)
        {
            sponge.stop = true;
            rocky.stop = true;
            liam.stop = true;
        }
    }
}
