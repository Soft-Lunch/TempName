using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationBar : MonoBehaviour
{

    public float speed = 5.0f;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.AngleAxis(speed * Time.time, Vector3.forward);
    }
}
