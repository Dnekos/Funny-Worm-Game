using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movehead : MonoBehaviour
{
    public float spd = 8f;
    int jumpHash = Animator.StringToHash("Jump");

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 change = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            change.y++;
        if (Input.GetKey(KeyCode.A))
            change.x--;
        if (Input.GetKey(KeyCode.S))
            change.y--;
        if (Input.GetKey(KeyCode.D))
            change.x++;

        //transform.LookAt(new Vector3(changex, changey),Vector3.forward);
        if (change != Vector3.zero)
        {
            float angle = Mathf.Atan2(change.y, change.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        transform.position += change * spd * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger(jumpHash);
            Debug.Log("pressed jup");
        }
    }

    public void Jump()
    {

    }
}
