using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") > 0)
        {
            pos = new Vector3(pos.x + cameraSpeed * Time.deltaTime, pos.y, pos.z);
        }
        if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") < 0)
        {
            pos = new Vector3(pos.x - cameraSpeed * Time.deltaTime, pos.y, pos.z);
        }
        if (Input.GetButton("Vertical") && Input.GetAxisRaw("Vertical") > 0)
        {
            pos = new Vector3(pos.x, pos.y + cameraSpeed * Time.deltaTime, pos.z);
        }
        if (Input.GetButton("Vertical") && Input.GetAxisRaw("Vertical") < 0)
        {
            pos = new Vector3(pos.x, pos.y - cameraSpeed * Time.deltaTime, pos.z);
        }

        transform.position = pos;
    }
}
