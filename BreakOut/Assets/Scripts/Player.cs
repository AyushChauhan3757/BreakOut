using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody _rb;

    void Start()
    {
        Cursor.visible= false;
        _rb = GetComponent<Rigidbody>();
    }

    
    void FixedUpdate()
    {
        _rb.MovePosition(new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0, 50)).x, -18, 0));
    }
}
