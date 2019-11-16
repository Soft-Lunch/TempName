using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCage : MonoBehaviour
{
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
    }

    public void OnPresure()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
}
