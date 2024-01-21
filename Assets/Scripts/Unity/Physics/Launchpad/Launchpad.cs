using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launchpad : MonoBehaviour
{
    public GameObject loadPrefab;
    public Transform launchPosition;

    public void Launch(float force, Vector2 direction)
    {
        Transform cursor = MouseCursor.GetInstance().transform;

        Fuel fuel = new Fuel(FuelType.STANDARD, 1000000f);

        LaunchData launchData = new LaunchData(direction, force, fuel);

        GameObject loadObj = GameObject.Instantiate(loadPrefab);
        loadObj.transform.position = launchPosition.position;
        loadObj.GetComponent<Launchable>().Launch(launchData);
    }

    public void Update()
    {
        Transform cursor = MouseCursor.GetInstance().transform;
        Vector2 directionLaunchpadToCursor = (cursor.position - transform.position).normalized;
        Vector2 currentPos = transform.position;
        Debug.DrawLine(currentPos, currentPos + directionLaunchpadToCursor * 10, Color.green);

        bool mouseClick = Input.GetMouseButtonDown(0);

        if(mouseClick)
        {
            this.Launch(0.01f, directionLaunchpadToCursor);
        }
    }
}
