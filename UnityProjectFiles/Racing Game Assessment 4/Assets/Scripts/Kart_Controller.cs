using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kart_Controller : MonoBehaviour
{

    [Header("Wheel Parts")]
    public WheelCollider _WheelFL;
    public WheelCollider _WheelFR;
    public WheelCollider _WheelRL;
    public WheelCollider _WheelRR;

    [Space]
    [Header("Controls")]
    public float _TopSpeed = 300.0f;
    public float _CurrentSpeed;
    public float _MaxTorque = 200.0f;
    public float _MaxSteerAngle = 20f;
    public float _MaxBrakeTorque = 2000.0f;
    public float _SpeedMultiplier = 2.5f;

    [Space]
    [Header("Input Names")]
    public string _ForwardAxes;
    public string _TurnAxes;
    public string _BrakeAxes;

    [Space]
    [Header("Components")]
    public Rigidbody _RB;
    public Vector3 _CenterOfMass;

    [Space]
    [Header("Racing Info")]
    public int _CurrentLap;
    public int _CurrentChekpoint;
    public int _CurrntPlace;

    [HideInInspector]
    public float _forward;
    [HideInInspector]
    public float _turn;
    [HideInInspector]
    public float _brake;

    private void Start()
    {

        _RB = GetComponent<Rigidbody>();
        _RB.centerOfMass = _CenterOfMass;

        _CurrentLap = 1;
        _CurrentChekpoint = 0;
    }

    private void FixedUpdate()
    {
        

        //wheel steering
        _WheelFL.steerAngle = _MaxSteerAngle * _turn;
        _WheelFR.steerAngle = _MaxSteerAngle * _turn;

        _CurrentSpeed = 2 * 22 / 7 * _WheelRR.radius * _WheelRR.rpm * 60 / 1000;

        if(_CurrentSpeed < _TopSpeed)
        {
            _WheelRL.motorTorque = (_MaxTorque * _SpeedMultiplier) * _forward;
            _WheelRR.motorTorque = (_MaxTorque * _SpeedMultiplier) * _forward;
        }
        
            _WheelFL.brakeTorque = _MaxBrakeTorque * _brake;
            _WheelFR.brakeTorque = _MaxBrakeTorque * _brake;
            _WheelRL.brakeTorque = _MaxBrakeTorque * _brake;
            _WheelRR.brakeTorque = _MaxBrakeTorque * _brake;

        
    }
}
