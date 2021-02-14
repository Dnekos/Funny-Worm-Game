using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public MoveHead Controller;
    public int index;

    private void FixedUpdate()
    {
        Controller.Grounded[index] = false;

        int layerMask = 1 << 6;
        //Debug.DrawRay(transform.position, Vector3.down * 1f,Color.green, 0.1f);
        if (Physics2D.Raycast(transform.position, Vector3.down, 1.3f, layerMask))
            Controller.Grounded[index] = true;
    }
}
