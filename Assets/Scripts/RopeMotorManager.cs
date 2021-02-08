using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeMotorManager : MonoBehaviour
{
    public float maxtimetoreverse = 0.4f;
    public float timetoreverse = 0.2f;
    public HingeJoint2D hinge;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timetoreverse <= 0)
        {
            timetoreverse = maxtimetoreverse;
            JointMotor2D motor = hinge.motor;
            motor.motorSpeed = -hinge.motor.motorSpeed;
            hinge.motor = motor;
        }
        timetoreverse -= Time.deltaTime;
    }
}
