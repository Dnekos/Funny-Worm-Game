using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteManager : MonoBehaviour
{
    public bool Grabbing = false;
    public DistanceJoint2D LockIn;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("biting down");

            switch (collision.tag)
            {
                case "Grabbable":
                    Debug.Log("grabbed object");
                    LockIn.connectedBody = collision.attachedRigidbody;
                    LockIn.enabled = true;
                    Grabbing = true;
                    break;
                case "Food":
                    Debug.Log("ate object");
                    Destroy(collision.gameObject);
                    break;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            LockIn.enabled = false;
            Grabbing = false;
        }
    }
}
