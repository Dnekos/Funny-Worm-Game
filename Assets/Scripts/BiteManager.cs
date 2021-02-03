using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteManager : MonoBehaviour
{
    public bool Grabbing = false;
    public int FoodEaten = 0;

    public DistanceJoint2D LockIn;
    public Animator anim;
    public Transform TargetAnchor;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.LeftShift) && !Grabbing)
        {
            anim.SetTrigger("Bite"); // start bite animation in Animator

            switch (collision.tag)
            {
                case "Grabbable": // if bit movable physics object, stay in place in relation to collision
                    Debug.Log("grabbed object");
                    LockIn.connectedBody = collision.attachedRigidbody;
                    LockIn.connectedAnchor = collision.attachedRigidbody.transform.InverseTransformPoint(TargetAnchor.position);
                    LockIn.enabled = true;
                    Grabbing = true;
                    break;
                case "Food": // if bit food, eat it
                    Debug.Log("ate object");
                    Destroy(collision.gameObject);
                    FoodEaten++; // increment food eaten
                    break;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift)) // releasing grab
        {
            LockIn.enabled = false;
            Grabbing = false;
        }
    }
}
