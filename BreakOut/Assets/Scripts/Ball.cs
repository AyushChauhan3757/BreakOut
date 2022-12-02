using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    float _speed = 20f; 
    Rigidbody _rb;
    Vector3 _velocity;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = Vector3.down * _speed;
    }

    
    void FixedUpdate()
    {
        _rb.velocity = _rb.velocity.normalized * _speed;
        _velocity = _rb.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        _rb.velocity = Vector3.Reflect(_velocity, collision.contacts[0].normal);
    }
}
