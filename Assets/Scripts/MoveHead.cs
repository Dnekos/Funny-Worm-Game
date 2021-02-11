using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
//using UnityEngine.U2D.IK; //needed if have to call the IKManager

public class MoveHead : MonoBehaviour
{
    public float spd = 12f;
    public bool onGround = false; // used for limiting air movement and (eventually) respawns

    public bool jumping = false;

    public Animation anime;
    Inputs controls;
    Rigidbody2D head;
    BiteManager biter;

    [Header("Pausing")]
    public GameObject canvas;
    public bool paused = false;

    [Header("Debug")]
    public bool MoveByForce = false; // toggles movement styles (rigidbody weight adjustments needed if switching)
    public bool MoveIfNotOnGround = true; // debug for testing GroundCheck

    public Vector3 MoveDirection;

    // space held positions
    Quaternion jump_angle;
    Vector3 jump_position;


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
        paused = !paused; // toggle pause
        canvas.SetActive(paused);

        // disable controls
        biter.Pause(paused);
        if (paused)
        {
            controls.Player.Move.Disable();
            controls.Player.Jump.Disable();
        }
        else
        {
            controls.Player.Move.Enable();
            controls.Player.Jump.Enable();
        }
    }

    void OnMove(Vector2 move_input)
    {
        MoveDirection = move_input;
    }

    void OnJump(float keypress)
    {
        if (keypress == 1 && onGround)
        {
            jumping = true;

            jump_angle = transform.rotation; // save direction for hold
            jump_position = anime.transform.position; // save position for hold

            anime.transform.rotation = jump_angle; // match direction with head

            anime.Play("Coil");
            Debug.Log("pressed jump");
        }
        if (keypress == 0)
        {
            anime.Play("Jump");
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

        if (!jumping && onGround && !paused)
        {
            if (MoveByForce)
                head.AddForce(MoveDirection * spd, ForceMode2D.Force);
            else
                transform.position += MoveDirection * spd * Time.deltaTime;
        }
        else if (jumping)
        {
            anime.transform.rotation = jump_angle; // match direction
            anime.transform.position = jump_position; // match position (to stop sliding)
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