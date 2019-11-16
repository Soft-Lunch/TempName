﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;

    public float smooth = 0.05f;

    Vector2 velocity;
    Vector3 offset;
    
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - playerTransform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float posX = Mathf.SmoothDamp(transform.position.x, playerTransform.position.x + offset.x, ref velocity.x, smooth);
        float posY = Mathf.SmoothDamp(transform.position.y, playerTransform.position.y + offset.y, ref velocity.y, smooth);

        transform.position = new Vector3(posX, posY, transform.position.z);
    }
}
