using UnityEngine;

public class KartController : MonoBehaviour
{

    [SerializeField] Rigidbody _TheRB;
    [SerializeField] float _ForwardAccel, _ReverseAccel, _MaxSpeed, _TurnStrenth, _GravityForce, _DragOnGround;


    float _SpeedInput, _TurnInput;
    bool _Grounded;


    [SerializeField] LayerMask _WhatIsGround;
    [SerializeField] float _GroundRayLenth = 0.5f;
    [SerializeField] Transform _GroundPoint;

    [SerializeField] Transform _LeftFrontWheel, _RightFrontWheel;
    [SerializeField] float _MaxWheelTurn;

    void Start()
    {
        _TheRB.transform.parent = null;
    }

    private void Update()
    {

        _SpeedInput = 0;
        if (Input.GetAxis("Acceleration") > 0)
        {
            _SpeedInput = Input.GetAxis("Acceleration") * _ForwardAccel * 1000f;

        }
        else if (Input.GetAxis("Acceleration") < 0)
        {
            _SpeedInput = Input.GetAxis("Acceleration") * _ReverseAccel * 1000f;

        }

        _TurnInput = Input.GetAxis("Horizontal");
        if (_Grounded)
        {

            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0.0f, _TurnInput * _TurnStrenth * Time.deltaTime * Input.GetAxis("Acceleration"), 0f));
        }

        _LeftFrontWheel.localRotation = Quaternion.Euler(_LeftFrontWheel.localRotation.eulerAngles.x, (_TurnInput * _MaxWheelTurn) - 180, _LeftFrontWheel.localRotation.eulerAngles.z);
        _RightFrontWheel.localRotation = Quaternion.Euler(_RightFrontWheel.localRotation.eulerAngles.x, _TurnInput * _MaxWheelTurn, _RightFrontWheel.localRotation.eulerAngles.z);

        transform.position = _TheRB.position;
    }

    void FixedUpdate()
    {
        _Grounded = false;
        RaycastHit hit;

        if (Physics.Raycast(_GroundPoint.position, -transform.up, out hit, _GroundRayLenth, _WhatIsGround))
        {
            _Grounded = true;

            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }

        if (_Grounded)
        {
            _TheRB.drag = _DragOnGround;
            if (Mathf.Abs(_SpeedInput) > 0f)
            {
                _TheRB.AddForce(transform.forward * _SpeedInput);
            }
        }
        else
        {
            _TheRB.drag = 0.1f;
            _TheRB.AddForce(Vector3.up * -_GravityForce * 100f);
        }
    }
}
