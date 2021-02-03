using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public MoveHead Controller;
    private void OnCollisionStay2D(Collision2D collision)
    {
        // return true if collider hits ground
        if (collision.collider.tag == "Ground" || collision.collider.tag == "Grabbable")
            Controller.onGround = true;
    }
}
