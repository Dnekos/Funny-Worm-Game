using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHead : MonoBehaviour
{
    public float spd = 12f;
    public bool onGround = false;

    public Animation anime;
    public Rigidbody2D head;
    BiteManager biter;

    [Header("Debug")]
    public bool MoveByForce = false; // toggles movement styles (rigidbody weight adjustments needed if switching)
    public bool MoveIfNotOnGround = true; // debug for testing GroundCheck

    // Start is called before the first frame update
    void Start()
    {
        biter = GetComponent<BiteManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 change = Vector3.zero;

        // get movement inputs (TODO: condense into function as it is used by JumpHandler)
        if (Input.GetKey(KeyCode.W))
            change.y++;
        if (Input.GetKey(KeyCode.A))
            change.x--;
        if (Input.GetKey(KeyCode.S))
            change.y--;
        if (Input.GetKey(KeyCode.D))
            change.x++;

        //transform.LookAt(new Vector3(changex, changey),Vector3.forward);
        if (change != Vector3.zero && (onGround || MoveIfNotOnGround))
        {
            if (!biter.Grabbing)
            {
                float angle = Mathf.Atan2(change.y, change.x) * Mathf.Rad2Deg;

                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            if (MoveByForce)
                head.AddForce(change * spd, ForceMode2D.Force);
            else
                transform.position += change * spd * Time.deltaTime;
        }



        if (Input.GetKeyDown(KeyCode.Space) && anime.isPlaying == false && onGround)
        {
            float angle = Mathf.Atan2(transform.position.y, transform.position.x) * Mathf.Rad2Deg;
            anime.transform.rotation = transform.rotation;//Quaternion.AngleAxis(angle, Vector3.forward);

            anime.Play("Jump");
            Debug.Log("pressed jump");
        }

        onGround = false;
    }
}
