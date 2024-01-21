using UnityEditor;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    private static MouseCursor instance;
    public void Awake()
    {
        instance = this;
    }

    public static MouseCursor GetInstance()
    {
        if (instance == null) throw new System.Exception("Mouse Cursor Instance Is Null");
        return instance;
    }

    public void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 mouseWorldPosition = CameraController.GetInstance().unityMainCamera.ScreenToWorldPoint(mousePos);
        transform.position = mouseWorldPosition;
    }
}