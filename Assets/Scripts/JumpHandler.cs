using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpHandler : MonoBehaviour
{
    public float speed = 10;
    public Rigidbody2D head;
    public void Jump()
    {
        Vector2 change = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
            change.y++;
        if (Input.GetKey(KeyCode.A))
            change.x--;
        if (Input.GetKey(KeyCode.S))
            change.y--;
        if (Input.GetKey(KeyCode.D))
            change.x++;
        if (change == Vector2.zero)
            change = Vector2.up;

        head.AddForce(change * speed, ForceMode2D.Impulse);
        //head.velocity += change * speed;
        Debug.Log("jump");
    }
}
