using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int hits = 1;
    public int points = 100;
    //Rigidbody _rb;
    public Vector3 rotator;

    void Start()
    {
        //_rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        transform.Rotate(rotator * Time.deltaTime);
    }

    private async void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            hits--;
        }
        //Score
        if(hits<=0)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            transform.position += Vector3.down *Time.deltaTime * 50;
            await Task.Delay(800);
            Destroy(gameObject);
            
            
        }
    }
}
