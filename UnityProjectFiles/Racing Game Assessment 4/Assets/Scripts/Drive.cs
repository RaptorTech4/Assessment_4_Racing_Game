using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour
{
    [Header("Collider")]
    [SerializeField] WheelCollider FR;
    [SerializeField] WheelCollider FL;
    [SerializeField] WheelCollider RR;
    [SerializeField] WheelCollider RL;

    [Header("Model")]
    [SerializeField] GameObject FRM;
    [SerializeField] GameObject FLM;
    [SerializeField] GameObject RRM;
    [SerializeField] GameObject RLM;
    [Space]
    [SerializeField] float torque = 200;
    [SerializeField] float maxSteerAngel = 30f;
    [SerializeField] float maxBrack = 30f;


    void Start()
    {
        
    }

    void go(float accel, float steer, float brake)
    {
        accel = Mathf.Clamp(accel, -1, 1);
        brake = Mathf.Clamp(brake, 0, 1) * maxBrack;
        steer = Mathf.Clamp(steer, -1, 1) * maxSteerAngel;
        float thrustTorque = accel * torque;

        FL.steerAngle = steer;
        FR.steerAngle = steer;
        RR.motorTorque = thrustTorque;
        RL.motorTorque = thrustTorque;
        //FR.motorTorque = thrustTorque;
        //FL.motorTorque = thrustTorque;
        RR.brakeTorque = brake;
        RL.brakeTorque = brake;

        Quaternion quat;
        Vector3 pos;

        FL.GetWorldPose(out pos, out quat);
        FLM.transform.position = pos;
        FLM.transform.rotation = quat;
        FR.GetWorldPose(out pos, out quat);
        FRM.transform.position = pos;
        FRM.transform.rotation = quat;
        RR.GetWorldPose(out pos, out quat);
        RRM.transform.position = pos;
        RRM.transform.rotation = quat;
        RL.GetWorldPose(out pos, out quat);
        RLM.transform.position = pos;
        RLM.transform.rotation = quat;
    }

    void Update()
    {
        float a = Input.GetAxis("Acceleration");
        float b = Input.GetAxis("Brake");
        float s = Input.GetAxis("Horizontal");
        go(a,s,b);
    }
}
