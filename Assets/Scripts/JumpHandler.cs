using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpHandler : MonoBehaviour
{
    public GameObject first_bone,last_bone;

    [SerializeField]
    float minspeed = 5;
    public float speed = 10;

    [SerializeField]
    Rigidbody2D head;
    [SerializeField]
    MoveHead head_controller;

    [SerializeField]
    float max_jump_time = 1f;
    float jump_timer = 0;

    private void Update()
    {
        if (head_controller.jumping) // increment time as space is held
            jump_timer += Time.deltaTime;
    }

    public void Jump()
    {
        foreach(Transform bone in first_bone.GetComponentsInChildren<Transform>())
        {
            bone.localPosition = new Vector3(2.5f, 0, 0);
        }
        last_bone.transform.localPosition = new Vector3(3, 0, 0);

        head.AddForce(head_controller.MoveDirection * Mathf.Lerp(minspeed, speed, jump_timer / max_jump_time), // lerp is to have rampup for chargedjump
            ForceMode2D.Impulse); // add force in direction of keys
        head_controller.jumping = false; // enable movement again

        Debug.Log("jumped with force of " + Mathf.Lerp(minspeed, speed, jump_timer / max_jump_time) + " " + jump_timer + "/" + max_jump_time);
        jump_timer = 0; // reset timer

    }

    public void HoldPosition()
    {
        GetComponent<Animation>().Play("Coil_Hold");
    }
}
