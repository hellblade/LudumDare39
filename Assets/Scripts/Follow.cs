using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;

    float borderX;
    float borderY;

    MapGenerator map;

    void Start()
    {
        map = FindObjectOfType<MapGenerator>();

        var cam = Camera.main;

        var bottomLeft = cam.ScreenToWorldPoint(Vector3.zero);
        var topRight = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, 0));

        var diff = topRight - bottomLeft;

        borderX = Mathf.Abs(diff.x) / 2;
        borderY = Mathf.Abs(diff.y) / 2;
    }

    void FixedUpdate()
    {
        Vector3 position = target.position;

        // Clamp it at the edges...
        if (position.y >= map.height - borderY)
        {
            position.y = map.height - borderY;
        }
        else if (position.y <= -map.height+ borderY)
        {
            position.y = -map.height + borderY;
        }

        if (position.x >= map.width - borderX)
        {
            position.x = map.width - borderX;
        }
        else if (position.x <= -map.width + borderX)
        {
            position.x = -map.width + borderX;
        }

        transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * 5);
    }
}
