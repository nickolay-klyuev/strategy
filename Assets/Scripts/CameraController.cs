using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed = 1f;
    public float cameraSizeStep = .5f;
    public float maxCameraSize = 7f;
    public float minCameraSize = 3f;
    public GameObject background;
    private Vector2 bgSize;

    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponent<Camera>();

        bgSize = background.GetComponent<SpriteRenderer>().size;
    }

    // Update is called once per frame
    void Update()
    {
        // camera move by WASD
        Vector3 pos = transform.position;
        float camHorizontalSize = mainCamera.orthographicSize * mainCamera.aspect;
        
        if (pos.x < bgSize.x - camHorizontalSize && Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") > 0)
        {
            pos = new Vector3(pos.x + cameraSpeed * Time.deltaTime, pos.y, pos.z);
        }
        if (pos.x > camHorizontalSize && Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") < 0)
        {
            pos = new Vector3(pos.x - cameraSpeed * Time.deltaTime, pos.y, pos.z);
        }
        if (pos.y < bgSize.y - mainCamera.orthographicSize && Input.GetButton("Vertical") && Input.GetAxisRaw("Vertical") > 0)
        {
            pos = new Vector3(pos.x, pos.y + cameraSpeed * Time.deltaTime, pos.z);
        }
        if (pos.y > mainCamera.orthographicSize && Input.GetButton("Vertical") && Input.GetAxisRaw("Vertical") < 0)
        {
            pos = new Vector3(pos.x, pos.y - cameraSpeed * Time.deltaTime, pos.z);
        }

        transform.position = pos;

        // chage camera size by mouse wheel (zoom)
        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0 && mainCamera.orthographicSize < maxCameraSize)
        {
            mainCamera.orthographicSize += cameraSizeStep;
        }
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0 && mainCamera.orthographicSize > minCameraSize)
        {
            mainCamera.orthographicSize -= cameraSizeStep;
        }
    }
}
