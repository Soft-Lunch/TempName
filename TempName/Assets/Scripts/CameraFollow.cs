using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;

    public float smooth = 0.5f;

    Vector3 offset;
    
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredPos = playerTransform.position + offset;

        transform.position = Vector3.Lerp(transform.position, desiredPos, smooth);
        
    }
}
