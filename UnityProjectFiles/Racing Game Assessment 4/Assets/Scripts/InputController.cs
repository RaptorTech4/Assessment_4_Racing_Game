using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    public string _inputSteerAxis = "";
    public string _inputThrottleAxis = "";

    public float _ThottelInput { get; private set; }
    public float _SteerInput { get; private set; }

    void Start()
    {
        
    }

    void Update()
    {
        _SteerInput = Input.GetAxis(_inputSteerAxis);
        _ThottelInput = Input.GetAxis(_inputThrottleAxis);


    }
}
