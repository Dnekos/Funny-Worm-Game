using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float speed = 0.01f;
    public Transform worm;
    Camera cam;

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
    }
}
