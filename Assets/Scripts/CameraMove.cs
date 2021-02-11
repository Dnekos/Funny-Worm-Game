using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float speed = 0.01f;
    public Transform worm;
    Camera cam;
    Vector3 offset = new Vector3(0, 2f, -10f);

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, worm.position) > cam.orthographicSize * 0.7)
        {
            transform.position = Vector2.Lerp(transform.position, worm.position, speed);
            transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }

        Vector3 target_position = worm.position + offset; // offset destination for camera (so it doesnt go into worm)
        float distance = Vector3.Distance(transform.position, target_position); // get distance from current to desired position

        if (distance > cam.orthographicSize * 0.6) // move to worm when moving offscreen
            transform.position = Vector3.Lerp(transform.position, target_position, speed);
        else if (distance > cam.orthographicSize * 0.4) // slow down to make it less jittery
            transform.position = Vector3.Lerp(transform.position, target_position, speed / 2);
    }
}