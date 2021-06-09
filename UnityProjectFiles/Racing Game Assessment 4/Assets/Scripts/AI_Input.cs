using System.Collections.Generic;
using UnityEngine;

public class AI_Input : MonoBehaviour
{

    Kart_Controller _kart;
    public float _CurrentSpeed = 0.5f;

    public Transform _Path;
    public float _NodeDis = 0.5f;

    List<Transform> nodes;
    int _CurrentNode = 0;

    public float _TurnSpeed = 1.5f;

    [Header("sensor")]
    public float _SensorLenth = 7.5f;
    public Vector3 _FrontVectorPos = new Vector3(0.0f, 0.5f, 0.0f);
    public float _FrontSensorPos = 0.5f;
    public float _SideSensorPos = 0.5f;
    public float _FrontAnglePos = 30f;

    bool _Avoid = false;
    float _AvoidMulti;
    float _TargetTurnAngel;

    private void Start()
    {
        _kart = GetComponent<Kart_Controller>();

        Transform[] pathTransforms = _Path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != _Path.transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }
    }

    private void FixedUpdate()
    {
        DriveAI();
        Check();
        SteerAI();
        SmoveTurn();

        _kart._brake = 0.0f;

        //WayPointCheck();
    }

    void DriveAI()
    {
        _kart._forward = _CurrentSpeed;
    }

    void SteerAI()
    {
        if (_Avoid)
        {
            _TargetTurnAngel = _AvoidMulti;
        }
        else
        {
            Kart_Controller kart = GetComponent<Kart_Controller>();

            _CurrentNode = kart._CurrentChekpoint;

            Vector3 relativeVector = transform.InverseTransformPoint(nodes[_CurrentNode].position);
            float steerAmount = (relativeVector.x / relativeVector.magnitude);
            _TargetTurnAngel = steerAmount;
        }
    }

    //void WayPointCheck()
    //{
    //    if (Vector3.Distance(transform.position, nodes[_CurrentNode].position) < _NodeDis)
    //    {
    //        if (_CurrentNode == nodes.Count - 1)
    //        {
    //            _CurrentNode = 0;
    //        }
    //        else
    //        {
    //            _CurrentNode++;
    //        }
    //    }
    //}

    void Check()
    {
        RaycastHit hit;
        Vector3 sensorStartPos = transform.position;
        sensorStartPos += transform.forward * _FrontVectorPos.z;
        sensorStartPos += transform.up * _FrontVectorPos.x;

        _AvoidMulti = 0.0f;
        _Avoid = false;


        // right
        sensorStartPos += transform.right * _FrontSensorPos;
        if (Physics.Raycast(sensorStartPos, transform.right, out hit, _SensorLenth))
        {
            if (hit.collider.tag == "Obstacle" || hit.collider.tag == "Kart" && hit.collider != this)
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                _Avoid = true;
                _AvoidMulti -= 1.0f;
            }
        }
        //right angel
        if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(_FrontAnglePos, transform.up) * transform.forward, out hit, _SensorLenth))
        {
            if (hit.collider.tag == "Obstacle" || hit.collider.tag == "Kart" && hit.collider != this)
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                _Avoid = true;
                _AvoidMulti -= 0.5f;
            }
        }

        // left
        sensorStartPos -= transform.right * _FrontSensorPos * 2;
        if (Physics.Raycast(sensorStartPos, -transform.right, out hit, _SensorLenth))
        {
            if (hit.collider.tag == "Obstacle" || hit.collider.tag == "Kart" && hit.collider != this)
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                _Avoid = true;
                _AvoidMulti += 1.0f;
            }
        }
        //left angel
        if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(-_FrontAnglePos, transform.up) * transform.forward, out hit, _SensorLenth))
        {
            if (hit.collider.tag == "Obstacle" || hit.collider.tag == "Kart" && hit.collider != this)
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                _Avoid = true;
                _AvoidMulti += 0.5f;
            }
        }

        if (_AvoidMulti == 0.0f)
        {

            //front
            if (Physics.Raycast(sensorStartPos, transform.forward, out hit, _SensorLenth))
            {
                if (hit.collider.tag == "Obstacle" || hit.collider.tag == "Kart" && hit.collider != this)
                {
                    Debug.DrawLine(sensorStartPos, hit.point);
                    _Avoid = true;
                    if (hit.normal.x < 0.0f)
                    {
                        _AvoidMulti = -1.0f;
                    }
                    else
                    {
                        _AvoidMulti = 1.0f;
                    }

                   
                }
            }
        }
    }

    void SmoveTurn()
    {
        _kart._turn = Mathf.Lerp(_kart._turn, _TargetTurnAngel, Time.fixedDeltaTime * _TurnSpeed);
    }
}
