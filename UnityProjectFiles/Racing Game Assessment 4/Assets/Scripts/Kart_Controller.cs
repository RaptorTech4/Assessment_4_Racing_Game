using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kart_Controller : MonoBehaviour
{

    [Header("Wheel Parts")]
    [SerializeField] WheelCollider _WheelFL;
    [SerializeField] WheelCollider _WheelFR;
    [SerializeField] WheelCollider _WheelRL;
    [SerializeField] WheelCollider _WheelRR;

    [Space]
    [Header("Controls")]
    [SerializeField] float _MaxSpeed = 300f;
    [SerializeField] float _CurrentSpeed;
    [SerializeField] float _MaxTorque = 200f;
    [SerializeField] float _MaxSteerAngle = 30f;
    [SerializeField] float _MaxBrakeTorque = 2000f;
    [SerializeField] float _SpeedMultiplier = 2.5f;

    float _Forward;
    float _Turn;
    float _Brake;

    private void FixedUpdate()
    {
        _Forward = Input.GetAxis("Acceleration");
        _Turn = Input.GetAxis("Horizontal");
        _Brake = Input.GetAxis("Brake");

        _WheelFL.steerAngle = _MaxSteerAngle * _Turn;
        _WheelFR.steerAngle = _MaxSteerAngle * _Turn;

        _CurrentSpeed = 2 * 22 / 7 * _WheelRL.radius * _WheelRL.rpm * 60 / 1000;

        if(_CurrentSpeed <= _MaxSpeed)
        {
            _WheelRL.motorTorque = (_MaxTorque * _SpeedMultiplier) * _Forward;
            _WheelRR.motorTorque = (_MaxTorque * _SpeedMultiplier) * _Forward;
        }


        _WheelFL.brakeTorque = _MaxBrakeTorque * _Brake;
        _WheelFR.brakeTorque = _MaxBrakeTorque * _Brake;
        _WheelRL.brakeTorque = _MaxBrakeTorque * _Brake;
        _WheelRR.brakeTorque = _MaxBrakeTorque * _Brake;
        
    }
}
