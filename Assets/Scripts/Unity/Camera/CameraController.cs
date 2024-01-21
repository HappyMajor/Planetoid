using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public bool CanZoom { get { return canZoom; } set { canZoom = value; } }
    public bool CanMoveWithArrowKeys { get { return canMoveWithArrowKeys; } set { canMoveWithArrowKeys = value; } }

    public Cinemachine.CinemachineVirtualCamera virtualCamera;
    public Camera unityMainCamera;
    public float zoomAmount = 0f;
    public float minClamp = 3f;
    public float maxClamp = 50f;
    public float zoom = 4f;
    public float smoothing = 0.1f;
    public float zoomStep = 1f;
    public float zoomLevelOneThreshold = 12f;
    public float zoomLevelOne = 22f;
    public float cursorSpeedZoomLevelOne = 16f;
    public float cursorSpeedZoomLevelZero = 5f;
    public float cursorSpeedZoomLevelTwo = 32f;
    public float zoomLevelTwoThreshold = 32f;
    public float zoomLevelTwo = 45f;
    private static CameraController instance;
    private ArrowKeyCursor arrowCursor;
    private bool canMoveWithArrowKeys = true;
    private bool canZoom = true;
    public void Start()
    {
        this.arrowCursor = (ArrowKeyCursor) GameObject.FindAnyObjectByType(typeof(ArrowKeyCursor));

        if (this.arrowCursor == null) throw new System.Exception("No Cursor Found");
        if (this.virtualCamera == null) throw new System.Exception("No Virtual Camera Set");
        if (this.unityMainCamera == null) throw new System.Exception("No UnityMainCamera Set");
    }

    public void Update()
    {
        if (this.CanMoveWithArrowKeys && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))) {
            if(virtualCamera.Follow.transform != arrowCursor.transform)
            {
                if(virtualCamera.Follow.position != null)
                {
                    this.arrowCursor.transform.position = virtualCamera.Follow.position;
                }
                this.FollowArrowCursor();
            }
        }
        if(this.CanZoom)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                zoom += zoomStep;
                if(zoom >= zoomLevelOneThreshold && zoom <= zoomLevelOne)
                {
                    Debug.Log("ZOOM LEVEL ONE!");
                    zoom = zoomLevelOne + zoomStep;
                    this.arrowCursor.speed = cursorSpeedZoomLevelOne;
                }
                if(zoom >= zoomLevelTwoThreshold && zoom <= zoomLevelTwo)
                {
                    Debug.Log("ZOOM LEVEL TWO!");
                    zoom = zoomLevelTwo + zoomStep;
                    this.arrowCursor.speed = cursorSpeedZoomLevelTwo;
                }
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                zoom -= zoomStep;
                if(zoom >= zoomLevelOneThreshold && zoom <= zoomLevelOne)
                {
                    zoom = zoomLevelOneThreshold - zoomStep;
                    this.arrowCursor.speed = cursorSpeedZoomLevelZero;
                }
                if( zoom >= zoomLevelTwoThreshold && zoom < zoomLevelTwo)
                {
                    zoom = zoomLevelTwoThreshold - zoomStep;
                    this.arrowCursor.speed = cursorSpeedZoomLevelOne;
                }
            }
        }

        zoom = Mathf.Clamp(zoom, minClamp, maxClamp);
        virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, zoom, smoothing * Time.deltaTime);
    }

    public void Awake()
    {
        instance = this;
    }

    public static CameraController GetInstance()
    {
        if (instance == null) throw new System.Exception("Instance Of CameraController Is Null");
        return instance;
    }

    public void Follow(Transform transform)
    {
        if(transform == null)
        {
            if(arrowCursor == null)
            {
                throw new System.Exception("ArrowKeyCursor is null");
            }
            this.arrowCursor.transform.position = virtualCamera.Follow.transform.position;
            virtualCamera.Follow = null;
            this.FollowArrowCursor();
        } else
        {
            virtualCamera.Follow = transform;
        }
    }

    public Transform FollowTarget()
    {
        return virtualCamera.Follow;
    }

    public void FollowArrowCursor()
    {
        this.Follow(arrowCursor.transform);
    }

    public void ZoomIn()
    {

    }

    public void ZoomOut()
    {

    }

}
