using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float speed = 0.125f;
    public Transform worm;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Vector2.Distance(transform.position, worm.position) > cam.orthographicSize * 0.6)
        {
            transform.position = Vector2.Lerp(transform.position, worm.position, speed);
            transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }
        else if (Vector2.Distance(transform.position, worm.position) > cam.orthographicSize * 0.4)
        {
            transform.position = Vector2.Lerp(transform.position, worm.position, speed / 2);
            transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }
    }
}
