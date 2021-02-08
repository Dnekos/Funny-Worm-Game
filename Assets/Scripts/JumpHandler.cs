using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpHandler : MonoBehaviour
{
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
        if (head_controller.jumping)
            jump_timer += Time.deltaTime;
    }

    public void Jump()
    {
        head.AddForce(head_controller.MoveDirection * Mathf.Lerp(minspeed, speed, jump_timer / max_jump_time), // lerp is to have rampup for chargedjump
            ForceMode2D.Impulse); // add force in direction of keys
        jump_timer = 0;
        head_controller.jumping = false;

        Debug.Log("jump");
    }

    public void HoldPosition()
    {
        GetComponent<Animation>().Play("Coil_Hold");
    }
}
