using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
//using UnityEngine.U2D.IK; //needed if have to call the IKManager

public class MoveHead : MonoBehaviour
{
    public float spd = 12f;

    public bool jumping = false;

    public Animation anime;
    Inputs controls;
    Rigidbody2D head;
    Rigidbody2D tail;
    BiteManager biter;

    [Header("Pausing")]
    public GameObject canvas;
    public bool paused = false;

    [Header("Debug")]
    public bool MoveByForce = false; // toggles movement styles (rigidbody weight adjustments needed if switching)
    public bool MoveIfNotOnGround = true; // debug for testing GroundCheck

    public Vector3 MoveDirection; // used for limiting air movement

    // space held positions
    Quaternion jump_angle;
    Vector3 jump_position;

    public bool[] Grounded;

    /// <summary>
    /// returns true if any bones are close to the ground
    /// </summary>
    /// <returns></returns>
    bool OnGound()
    {
        foreach (bool bone in Grounded)
            if (bone)
                return true;
        return false;
    }

    /// <summary>
    /// connect inputs to functions
    /// </summary>
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
        tail = anime.GetComponent<Rigidbody2D>();
    }

    public void OnPause()
    {
        paused = !paused; // toggle pause
        canvas.SetActive(paused);
        AudioManager.PlayButtonSFX();

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
        if (keypress > 0 && OnGound() && !biter.Grabbing)
        {
            jumping = true;

            jump_angle = transform.rotation; // save direction for hold
            jump_position = anime.transform.position; // save position for hold

            anime.transform.rotation = jump_angle; // match direction with head

            anime.Play("Coil");
        }
        if (keypress == 0 && jumping)
            anime.Play("Jump");
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // capping rigidbodies
        if (head.velocity.magnitude > 60f)
            head.velocity = head.velocity.normalized * 60f;
        if (tail.velocity.magnitude > 60f)
            tail.velocity = tail.velocity.normalized * 60f;

        // rotating head
        if (!biter.Grabbing && MoveDirection != Vector3.zero) // don't rotate head if attached to physicsobject
        {
            float angle = Mathf.Atan2(MoveDirection.y, MoveDirection.x) * Mathf.Rad2Deg; // turn unitvector to angle
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); // set rotation of head
        }

        if (!jumping && !paused)
        {
            if (OnGound())
                transform.position += MoveDirection * spd * Time.deltaTime;
            else // allow for restricted movement when in air
                transform.position += MoveDirection * spd * 0.3f * Time.deltaTime;

        }
        else if (jumping) // charging jump
        {
            anime.transform.rotation = jump_angle; // match direction
            anime.transform.position = jump_position; // match position (to stop sliding)
            head.velocity = Vector2.zero; // Stop rigidbodies from moving
            tail.velocity = Vector2.zero;
        }

        // if fallen too low, restart scene
        if (transform.position.y < -100f)
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