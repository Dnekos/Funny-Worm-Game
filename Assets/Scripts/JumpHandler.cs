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

    [Header("Charge Color")]
    [SerializeField]
    SpriteRenderer wormbody;
    [SerializeField]
    SpriteRenderer wormhead;
    [SerializeField]
    Color chargecolor;

    private void Update()
    {
        if (head_controller.jumping) // increment time as space is held
        {
            jump_timer += Time.deltaTime;
            wormbody.color = Color.Lerp(Color.white, chargecolor, jump_timer / max_jump_time);
            wormhead.color = Color.Lerp(Color.white, chargecolor, jump_timer / max_jump_time);            
        }
    }

    public void Jump()
    {
        head.AddForce(head_controller.MoveDirection * Mathf.Lerp(minspeed, speed, jump_timer / max_jump_time), // lerp is to have rampup for chargedjump
            ForceMode2D.Impulse); // add force in direction of keys
        head_controller.jumping = false; // enable movement again

        wormbody.color = Color.white;
        wormhead.color = Color.white;

        Debug.Log("jumped with force of " + Mathf.Lerp(minspeed, speed, jump_timer / max_jump_time) + " " + jump_timer + "/" + max_jump_time);
        jump_timer = 0; // reset timer
    }

    public void HoldPosition()
    {
        GetComponent<Animation>().Play("Coil_Hold");
    }
}
