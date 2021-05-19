using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{

    [SerializeField] Transform _Target;
    [SerializeField] float _Distance;
    [SerializeField] float _MaxDriftRange;
    [SerializeField] float _AngleX;
    [SerializeField] float _AngleY;

    Transform _M_Transform_Cache;
    Transform _MyTransform
    {
        get
        {
            if(_M_Transform_Cache == null)
            {
                _M_Transform_Cache = transform;
            }
            return _M_Transform_Cache;
        }
    }


    private void OnValidate()
    {
        if(_Target!=null)
        {
            Vector3 targetPos = GetTargetPos();
            _MyTransform.position = targetPos;
            _MyTransform.LookAt(_Target);
        }
    }



    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPos = GetTargetPos();

        float t = Vector3.Distance(_MyTransform.position, _Target.position / _MaxDriftRange);

        _MyTransform.position = Vector3.Lerp(_MyTransform.position, targetPos, t * Time.deltaTime);
        _MyTransform.LookAt(_Target);
    }

    Vector3 GetTargetPos()
    {
        Vector3 targetPos = new Vector3(0.0f, 0.0f, -_Distance);
        targetPos = Quaternion.Euler(_AngleX, _AngleY, 0.0f) * targetPos;
        return _Target.position + ( _Target.rotation * targetPos);
    }

}
