using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwerveController : MonoBehaviour
{
    private float _lastFrameFingerPositionX;
    private float _moveFactorX;
    public float MoveFactorX => _moveFactorX;
    public bool _isHolding = false; 

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _lastFrameFingerPositionX = Input.mousePosition.x;
            _isHolding = true;
        }

        else if (Input.GetMouseButton(0))
        {
            _moveFactorX = Input.mousePosition.x - _lastFrameFingerPositionX;
            _lastFrameFingerPositionX=Input.mousePosition.x;
        }

        else if (Input.GetMouseButtonUp(0))
        {
            _moveFactorX = 0;
            _isHolding = false;
        }
    }

}