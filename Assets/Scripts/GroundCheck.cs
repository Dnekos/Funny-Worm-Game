using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public MoveHead Controller;

    private void FixedUpdate()
    {
        int layerMask = 1 << 6;
        //Debug.DrawRay(transform.position, Vector3.down * 1f,Color.green, 0.1f);
        if (Physics2D.Raycast(transform.position, Vector3.down, 1f, layerMask))
        {
            Debug.Log("On Ground");
            Controller.onGround = true;
        }
    }
}
