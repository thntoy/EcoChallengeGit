using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothSpeed = 0.125f;

    [SerializeField] private float _minClampX;
    [SerializeField] private float _maxClampX;
    private void Update()
    {
        Vector3 desiredPosition = new Vector3(_target.position.x, transform.position.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
        transform.position = new Vector3(Mathf.Clamp(smoothedPosition.x, _minClampX, _maxClampX), smoothedPosition.y, smoothedPosition.z);
    }
}
