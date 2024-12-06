using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavyMovement : MonoBehaviour
{
    [SerializeField] private float _delay;

    [SerializeField] private float _frequency = 2;
    [SerializeField] private float _magnitude = 0.1f;

    private bool _isDelay = true;
    private Vector3 _defaultPos;

    private void Awake()
    {
        _defaultPos = transform.position;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, _defaultPos.y + Mathf.Sin(Time.time * _frequency) * _magnitude, transform.position.z);
    }
}
