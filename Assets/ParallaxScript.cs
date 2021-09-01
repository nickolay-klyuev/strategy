using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScript : MonoBehaviour
{
    public string paralaxType = "keyboard"; // mouse, keyboard
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
        float distX, distY;

        if (paralaxType == "keyboard")
        {
            distX = cam.transform.position.x * parallaxIndex;
            distY = cam.transform.position.y * parallaxIndex;
        }
        else
        {
            distX = (Input.mousePosition.x - Screen.width/2) / 1000 / parallaxIndex;
            distY = (Input.mousePosition.y - Screen.height/2) / 1000 / parallaxIndex;
        }

        transform.position = new Vector3(startPosition.x + distX, startPosition.y + distY, transform.position.z);
    }
}
