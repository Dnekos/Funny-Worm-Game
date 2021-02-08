using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MoveHead : MonoBehaviour
{
    public float spd = 12f;
    public bool onGround = false; // used for limiting air movement and (eventually) respawns

    public bool jumping = false;

    public Animation anime;
    Inputs controls;
    Rigidbody2D head;
    BiteManager biter;

    public GameObject canvas;
    bool paused = false;

    [Header("Debug")]
    public bool MoveByForce = false; // toggles movement styles (rigidbody weight adjustments needed if switching)
    public bool MoveIfNotOnGround = true; // debug for testing GroundCheck

    public Vector3 MoveDirection;
    Quaternion jump_angle;

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
        head = GetComponent<Rigidbody2D>();
        biter = GetComponent<BiteManager>();
    }

    public void OnPause()
    {
        paused = !paused;
        canvas.SetActive(paused);
    }

    void OnMove(Vector2 move_input)
    {
        MoveDirection = move_input;
    }

    void OnJump(float keypress)
    {
        if (keypress == 1)
        {
            jumping = true;

            jump_angle = transform.rotation; // save direction for hold
            anime.transform.rotation = jump_angle; // match direction with head
            //anime.transform.position = (transform.position + transform.position + anime.transform.position) / 3f; // move end of worm closer to head 
            anime.Play("Coil");
            Debug.Log("pressed jump");
        }
        if (keypress == 0  && onGround)
        {
 
            anime.Play("Jump"); // start animation (animation triggers event that calls JumpHandler
            Debug.Log("released jump");
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        if (!biter.Grabbing && MoveDirection != Vector3.zero) // don't rotate head if attached to physicsobject
        {
            float angle = Mathf.Atan2(MoveDirection.y, MoveDirection.x) * Mathf.Rad2Deg; // turn unitvector to angle
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); // set rotation of head
        }

        if (!jumping)
        {
            if (onGround)
            {
                if (MoveByForce)
                    head.AddForce(MoveDirection * spd, ForceMode2D.Force);
                else
                    transform.position += MoveDirection * spd * Time.deltaTime;
            }
        }
        else
        {
            anime.transform.rotation = jump_angle; // match direction
        }

        // if fallen too low, restart scene
        if (transform.position.y < -20f)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        onGround = false;
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