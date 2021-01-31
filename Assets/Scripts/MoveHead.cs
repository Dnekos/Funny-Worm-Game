using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHead : MonoBehaviour
{
    public float spd = 8f;
    public bool onGround = false;

    public Animation anime;
    public Rigidbody2D head;
    BiteManager biter;

    [Header("Debug")]
    public bool MoveByForce = false;
    public bool AlwaysOnGround = true;

    // Start is called before the first frame update
    void Start()
    {
        biter = GetComponent<BiteManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
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
        if (change != Vector3.zero && (onGround || AlwaysOnGround))
        {
            float angle = Mathf.Atan2(change.y, change.x) * Mathf.Rad2Deg;
            if (!biter.Grabbing)
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if (MoveByForce)
                head.AddForce(change * spd, ForceMode2D.Force);
            else
                transform.position += change * spd * Time.deltaTime;
        }



        if (Input.GetKeyDown(KeyCode.Space) && anime.isPlaying == false)
        {
            float angle = Mathf.Atan2(transform.position.y, transform.position.x) * Mathf.Rad2Deg;
            anime.transform.rotation = transform.rotation;//Quaternion.AngleAxis(angle, Vector3.forward);

            anime.Play("Jump");
            Debug.Log("pressed jump");
        }

        onGround = false;
    }
}
