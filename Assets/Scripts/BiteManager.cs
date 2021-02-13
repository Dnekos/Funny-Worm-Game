using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BiteManager : MonoBehaviour
{
    public bool Grabbing = false;
    public int FoodEaten = 0;

    public DistanceJoint2D LockIn;
    public Animator anim;
    [SerializeField]
    Transform TargetAnchor;

    [Header("UI Settings")]
    [SerializeField]
    bool EnabledCounter = false;
    [SerializeField]
    Transform CounterDisplay;
    int maxFood;
    
    //MoveHead inputs;
    Inputs controls;

    float shift_held = 0;

    private void Awake()
    {
        controls = new Inputs();
        controls.Player.Bite.performed += ctx => OnGrab(ctx.ReadValue<float>());

        if (EnabledCounter)
        {
            maxFood = GameObject.FindGameObjectsWithTag("Food").Length;
            CounterDisplay.localScale =  Vector3.zero;
        }
    }

    public void Pause(bool pause)
    {
        if (pause)
            controls.Player.Bite.Disable();
        else
            controls.Player.Bite.Enable();
    }

    private void OnGrab(float value)
    {
        Debug.Log("pressed shift");
        shift_held = value;
        if (value == 1) // key down
            anim.SetBool("ShiftKey", true); // start bite animation in Animator
        else // key up
            anim.SetBool("ShiftKey", false); // start bite animation in Animator
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (shift_held > 0.5 && !Grabbing)
        {
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
                    if (FoodEaten == maxFood) // if eaten all food, go to next level
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

                    //UI food tracker
                    if (EnabledCounter)
                        CounterDisplay.localScale = new Vector3(Mathf.Lerp(1, 0, FoodEaten / maxFood), 1, 1);

                    break;
            }
            // sets to another non-zero value to stop OnTriggerStay from grabbing multiple objects without letting go
            // its at -=0.1f so that it can go either 5 frames or have room for error if multiple collisions happen in the same frame
            // TODO: make this less scuffed
            shift_held -= 0.1f; 
        }
    }

    private void Update()
    {
        if (shift_held == 0) // releasing grab
        {
            LockIn.enabled = false;
            Grabbing = false;
        }
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
