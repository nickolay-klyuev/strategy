using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScript : MonoBehaviour
{
    public GameObject cam;
    public float parallaxIndex;

    private Vector2 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //float distX = cam.transform.position.x * parallaxIndex;
        //float distY = cam.transform.position.y * parallaxIndex;

        float distX = (Input.mousePosition.x - Screen.width/2) / 1000 / parallaxIndex;
        float distY = (Input.mousePosition.y - Screen.height/2) / 1000 / parallaxIndex;

        transform.position = new Vector3(startPosition.x + distX, startPosition.y + distY, transform.position.z);
    }
}
