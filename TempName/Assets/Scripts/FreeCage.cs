using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCage : MonoBehaviour
{
    public float timeToWait = 2.0f;
    public GameObject invisibleWall;

    float timeOpened;
    bool presured = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            OnPresure();
        }
        if (timeOpened + timeToWait <= Time.time && presured && invisibleWall)
        {
            presured = false;
            Destroy(invisibleWall);
        }
    }

    public void OnPresure()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        presured = true;
        timeOpened = Time.time;
    }
}
