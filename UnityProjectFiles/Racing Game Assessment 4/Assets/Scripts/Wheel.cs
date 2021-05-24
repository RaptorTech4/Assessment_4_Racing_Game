using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{

    public bool _Steer;
    public bool _InvertSteer;
    public bool _Power;

    public float _SteerAngle { get; set; }
    public float _Torque { get; set; }

    private WheelCollider _WheelCollider;
    private Transform _WheelTransform;

    void Start()
    {
        _WheelCollider = GetComponentInChildren<WheelCollider>();
        _WheelTransform = GetComponentInChildren<MeshRenderer>().GetComponent<Transform>();
    }

    void Update()
    {
        _WheelCollider.GetWorldPose(out Vector3 pos, out Quaternion rot);
        _WheelTransform.position = pos;
        _WheelTransform.rotation = rot;
    }

    private void FixedUpdate()
    {
        if(_Steer)
        {
            _WheelCollider.steerAngle = _SteerAngle * (_InvertSteer ? -1 : 1);
        }
        if(_Power)
        {
            _WheelCollider.motorTorque = _Torque;
            
        }
    }
}
