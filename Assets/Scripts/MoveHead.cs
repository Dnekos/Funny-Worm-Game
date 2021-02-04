using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MoveHead : MonoBehaviour
{
    public float spd = 12f;
    public bool onGround = false;

    public Inputs controls;
    public Animation anime;
    public Rigidbody2D head;
    BiteManager biter;

    [Header("Debug")]
    public bool MoveByForce = false; // toggles movement styles (rigidbody weight adjustments needed if switching)
    public bool MoveIfNotOnGround = true; // debug for testing GroundCheck

    private void Awake()
    {
        controls = new Inputs();
        controls.Player.Move.performed += ctx => OnMove(ctx.ReadValue<Vector2>());
        controls.Player.Pause.performed += ctx => OnPause();
        controls.Player.Jump.performed += ctx => OnJump(ctx.ReadValue<float>());

    }

    // Start is called before the first frame update
    void Start()
    {
        biter = GetComponent<BiteManager>();
    }

    void OnPause()
    {

    }

    void OnMove(Vector2 move_input)
    {
        Vector3 change = move_input;
        Debug.Log("moving" + change);
       
    }


    /* if (change != Vector3.zero && (onGround || MoveIfNotOnGround)) // if any of WASD is pressed
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
        }*/
    void OnJump(float keypress)
    {
        if (keypress == 1 && !anime.isPlaying)
        {

        }
        if (keypress == 0  && onGround)
        {
            //float angle = Mathf.Atan2(transform.position.y, transform.position.x) * Mathf.Rad2Deg;
            anime.transform.rotation = transform.rotation; // match direction with head
            anime.transform.position = (transform.position + transform.position + anime.transform.position) / 3f; // move end of worm closer to head 

            onGround = false; // compromise for now while working out GroundCheck
            anime.Play("Jump"); // start animation (animation triggers event that calls JumpHandler
            Debug.Log("pressed jump");
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {


        // if fallen too low, restart scene
        if (transform.position.y < -20f)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }
}