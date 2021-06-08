using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tire_To_Wheels : MonoBehaviour
{

    public WheelCollider wheelcol;

    void FixedUpdate()
    {
        Vector3 localpos = transform.localPosition;

        transform.localPosition = localpos;
        transform.localRotation = Quaternion.Euler(0, wheelcol.steerAngle, 0);
    }
}
