using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteManager : MonoBehaviour
{
    public int FoodEaten = 0;
    public bool Grabbing = false;
    public DistanceJoint2D LockIn;
    public Animator anim;
    public Transform TargetAnchor;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.LeftShift) && !Grabbing)
        {
            anim.SetTrigger("Bite");
            Debug.Log("biting down");

            switch (collision.tag)
            {
                case "Grabbable":
                    Debug.Log("grabbed object");
                    LockIn.connectedBody = collision.attachedRigidbody;
                    LockIn.connectedAnchor = collision.attachedRigidbody.transform.InverseTransformPoint(TargetAnchor.position);
                    LockIn.enabled = true;
                    Grabbing = true;
                    break;
                case "Food":
                    Debug.Log("ate object");
                    Destroy(collision.gameObject);
                    FoodEaten++;

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
