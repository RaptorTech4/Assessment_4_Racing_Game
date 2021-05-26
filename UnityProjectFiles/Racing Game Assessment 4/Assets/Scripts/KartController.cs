using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartController : MonoBehaviour
{

    public Transform _CenterOfMass;
    public float _MotorTorque = 1500f;
    public float _MaxSteer = 20f;
    public float _BrakeTorque = 2000f;

    public float _Steer { get; set; }
    public float _Throttle { get; set; }
    public float _Brake { get; set; }

    private Rigidbody _RB;
    private Wheel [] _Wheels;

    void Start()
    {
        _Wheels = GetComponentsInChildren<Wheel>();
        _RB = GetComponent<Rigidbody>();
        _RB.centerOfMass = _CenterOfMass.localPosition;
    }

    void Update()
    {

        _Steer = GameManager._Instance._InputController._SteerInput;
        _Throttle = GameManager._Instance._InputController._ThottelInput;
        _Brake = GameManager._Instance._InputController._BrakeInput;
        _RB.drag = _RB.velocity.magnitude / 250;
        foreach (var wheel in _Wheels)
        {
            wheel._SteerAngle = _Steer * _MaxSteer;
            wheel._Torque = _Throttle * _MotorTorque;
            wheel._BrakeTorque = _Brake * _BrakeTorque;
        }
    }
}
