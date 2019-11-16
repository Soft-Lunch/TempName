using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;

    public float smooth = 0.5f;
	public float offset_y = 0f;

    Vector2 velocity;
    Vector3 offset;
    
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - playerTransform.position;
		setOffsetY();
    }

    // Update is called once per frame
    void LateUpdate()
    {

        float posX = Mathf.SmoothDamp(transform.position.x, playerTransform.position.x + offset.x, ref velocity.x, smooth);
        float posY = Mathf.SmoothDamp(transform.position.y, playerTransform.position.y + offset.y, ref velocity.y, smooth);

        transform.position = new Vector3(posX, posY, transform.position.z);
    }
	
	void setOffsetY()
	{
		offset.y += offset_y;
	}

}
