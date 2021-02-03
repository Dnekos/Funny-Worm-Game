using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        if (change != Vector3.zero && (onGround || MoveIfNotOnGround)) // if any of WASD is pressed
        {
            if (!biter.Grabbing) // don't rotate head if attached to physicsobject
            {
                float angle = Mathf.Atan2(change.y, change.x) * Mathf.Rad2Deg; // turn unitvector to angle
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); // set rotation of head
            }
            if (MoveByForce)
                head.AddForce(change * spd, ForceMode2D.Force);
            else
                transform.position += change * spd * Time.deltaTime;
        }



        if (Input.GetKey(KeyCode.Space) && anime.isPlaying == false && onGround)
        {
            //float angle = Mathf.Atan2(transform.position.y, transform.position.x) * Mathf.Rad2Deg;
            anime.transform.rotation = transform.rotation; // match direction with head
            anime.transform.position = (transform.position + transform.position + anime.transform.position) / 3f; // move end of worm closer to head 

            onGround = false; // compromise for now while working out GroundCheck
            anime.Play("Jump"); // start animation (animation triggers event that calls JumpHandler
            Debug.Log("pressed jump");
        }

        // if fallen too low, restart scene
        if (transform.position.y < -20f)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}