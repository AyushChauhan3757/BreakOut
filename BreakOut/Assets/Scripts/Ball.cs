using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    float _speed = 20f; 
    Rigidbody _rb;
    Vector3 _velocity;
    Renderer _renderer;
    public AudioSource hitSound;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
        Invoke("Launch", 0.5f);
    }

    void Launch()
    {
        _rb.velocity = Vector3.up * _speed;
    }
    void FixedUpdate()
    {
        _rb.velocity = _rb.velocity.normalized * _speed;
        _velocity = _rb.velocity;

        if(!_renderer.isVisible) 
        {
            GameManager.Instance.Balls--;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _rb.velocity = Vector3.Reflect(_velocity, collision.contacts[0].normal);
        hitSound.Play();
    }
}
