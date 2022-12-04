using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int hits = 1;
    public int points = 100;
    Rigidbody _rb;
    BoxCollider _bc;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        hits--;
        //Score
        if(hits<=0)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            _rb.velocity = Vector3.down * 5;
            if (transform.position.y <= -20)
            {
                Destroy(gameObject);
            }
        }
    }
}
