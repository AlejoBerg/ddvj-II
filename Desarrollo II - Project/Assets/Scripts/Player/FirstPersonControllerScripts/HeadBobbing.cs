using System;
using UnityEngine;

public class HeadBobbing : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] private Transform _headTransform = null;
    [SerializeField] private Transform _cameraTransform = null;
    [SerializeField] private Rigidbody _playerRB = null;

    [Header("Head Bobbing")]
    [SerializeField] private float _bobFrecuency = 5f;
    [SerializeField] private float _bobHorizontalAmplitude = 0.1f;
    [SerializeField] private float _bobVerticalAmplitude = 0.1f;
    [Range(0, 1)] [SerializeField] private float _headBobSmoothing = 0.1f;

    private float _walkingTime;
    private Vector3 _targetCameraPosition;

    private void Update()
    {
        CheckWalkState();

         //Calculate camera target position
        _targetCameraPosition = _headTransform.position + CalculateHeadBobOffset(_walkingTime);

        //Interpolate positions
        _cameraTransform.position = Vector3.Lerp(_cameraTransform.position, _targetCameraPosition, _headBobSmoothing);

        //Snap to position if it is close enough
        if((_cameraTransform.position - _targetCameraPosition).magnitude <= 0.001f)
        {
            _cameraTransform.position = _targetCameraPosition;
        }
    }

    private Vector3 CalculateHeadBobOffset(float time)
    {
        float horizontalOffset = 0;
        float verticalOffset = 0;
        Vector3 offset = Vector3.zero;

        if(time > 0)
        {
            //Calculate offset 
            horizontalOffset = Mathf.Cos(time * _bobFrecuency) * _bobHorizontalAmplitude;
            verticalOffset = Mathf.Sin(time * _bobFrecuency * 2) * _bobVerticalAmplitude;

            //Combine offsets relative to head position and calculate the cameras target position
            offset = _headTransform.right * horizontalOffset + _headTransform.up * verticalOffset;
        }

        return offset;
    }

    private void CheckWalkState()
    {
        if (_playerRB.velocity.magnitude > 0.1f)
        {
            _walkingTime += Time.deltaTime;
        }
        else
        {
            _walkingTime = 0;
        }
    }
}
