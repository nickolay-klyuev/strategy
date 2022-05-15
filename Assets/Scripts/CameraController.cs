using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] SelectBoxController _selectBox;

    public float cameraSpeed = 1f;
    public float cameraSizeStep = .5f;
    public float maxCameraSize = 7f;
    public float minCameraSize = 3f;
    [SerializeField] float _touchCameraZoomIndex = 0.1f;
    public GameObject background;

    private Vector2 bgSize;

    private Camera mainCamera;

    private Vector2 _initialTouchPosition;
    private Vector3 _initialCameraPosition;

    private bool _changingCameraByTouches = false;
    private float _initialDistanceBetweenTouches;
    private float _initialCameraSize;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponent<Camera>();

        bgSize = background.GetComponent<SpriteRenderer>().size;
    }

    // Update is called once per frame
    void Update()
    {
        // chage camera size by mouse wheel (zoom)
        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0 && mainCamera.orthographicSize < maxCameraSize)
        {
            mainCamera.orthographicSize += cameraSizeStep;
            FixCameraPosition(2);
        }
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0 && mainCamera.orthographicSize > minCameraSize)
        {
            mainCamera.orthographicSize -= cameraSizeStep;
        }

        // change camera by touch
        if (Input.touchCount == 2)
        {
            if (!_changingCameraByTouches)
            {
                _initialDistanceBetweenTouches = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
                _initialCameraSize = mainCamera.orthographicSize;
                _changingCameraByTouches = true;
            }
            
            float currentDistanceBetweenTouches = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
            float cameraZoomAmount = currentDistanceBetweenTouches - _initialDistanceBetweenTouches;
            mainCamera.orthographicSize = _initialCameraSize - cameraZoomAmount * _touchCameraZoomIndex;
            FixCameraPosition(2);
        }
        else if (_changingCameraByTouches)
        {
            _changingCameraByTouches = false;
        }
    }

    void LateUpdate()
    {
        switch (SettingsScript.GetCurrentControlType())
        {
            case SettingsScript.ControlType.Touch:
                MoveCameraByTouch();
            break;

            case SettingsScript.ControlType.KeyboardMouse:
                MoveCameraByWASD();
            break;
        }        
    }

    private void MoveCameraByWASD()
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
    }

    private bool _movingByTouch = false;
    private bool _allowCameraMoving = true;
    private void MoveCameraByTouch()
    {
        UnitProperties possibleUnit = StaticMethods.GetGameObjectByRaycast().GetComponent<UnitProperties>();

        if (_selectBox.IsSelecting() || possibleUnit != null) // skip if selecting units
        {
            return;
        }

        if (_changingCameraByTouches) // skip if camera zooming
        {
            _allowCameraMoving = false;
            return;
        }

        // camera move by touch and drag
        if (Input.GetMouseButtonDown(0) && !_movingByTouch && _allowCameraMoving)
        {
            StartCoroutine(ForbidUnitsMove(0.1f));

            _movingByTouch = true;
            _initialTouchPosition = Input.mousePosition;
            _initialCameraPosition = mainCamera.transform.position;
        }

        if (_movingByTouch && _allowCameraMoving)
        {
            MoveCamera();
        }

        if (Input.GetMouseButtonUp(0))
        {
            _movingByTouch = false;
        }

        if (Input.touchCount < 2)
        {
            _allowCameraMoving = true;
        }
    }

    IEnumerator ForbidUnitsMove(float time)
    {
        yield return new WaitForSeconds(time);

        _selectBox.ForbidNextClickUp();
    }

    private void MoveCamera()
    {
        Vector3 cameraPos = mainCamera.transform.position;
        Vector3 newCameraPos = _initialCameraPosition + ((Vector3)_initialTouchPosition - Input.mousePosition) * 0.01f;
        float camHorizontalSize = mainCamera.orthographicSize * mainCamera.aspect;
        
        mainCamera.transform.position = newCameraPos;

        FixCameraPosition(2);
    }

    private void FixCameraPosition(int times = 1)
    {
        Vector3 cameraPos = mainCamera.transform.position;
        float camHorizontalSize = mainCamera.orthographicSize * mainCamera.aspect;

        if (bgSize.x - camHorizontalSize < cameraPos.x)
        {
            mainCamera.transform.position = new Vector3(bgSize.x - camHorizontalSize, cameraPos.y, cameraPos.z);
        }

        if (cameraPos.x < camHorizontalSize)
        {
            mainCamera.transform.position = new Vector3(camHorizontalSize, cameraPos.y, cameraPos.z);
        }

        if (cameraPos.y > bgSize.y - mainCamera.orthographicSize)
        {
            mainCamera.transform.position = new Vector3(cameraPos.x, bgSize.y - mainCamera.orthographicSize, cameraPos.z);
        }

        if (cameraPos.y < mainCamera.orthographicSize)
        {
            mainCamera.transform.position = new Vector3(cameraPos.x, mainCamera.orthographicSize, cameraPos.z);
        }

        if (times > 0)
        {
            FixCameraPosition(--times);
        }
    }
}
