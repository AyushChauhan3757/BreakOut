using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int hits = 1;
    public int points = 100;
    public Vector3 rotator;
    public Material hitmaterial;
    public Material[] MaterialList = new Material[5];

    Material _defaultmaterial;
    Renderer _renderer;

    void Start()
    {
        transform.Rotate(rotator *(transform.position.x + transform.position.y) * 0.1f);
        _renderer = GetComponent<Renderer>();
        _defaultmaterial= GetComponent<Renderer>().sharedMaterial;
    }

    
    void Update()
    {
        transform.Rotate(rotator * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
            hits--;
            if(hits<=0)
            {
                GameManager.Instance.Score += points;
                Destroy(gameObject);
            }
            _renderer.sharedMaterial = hitmaterial;
            Invoke("RestoreMaterial", 0.05f);
    }
    void RestoreMaterial()
    {
        _renderer.sharedMaterial =MaterialList[hits-1];
    }
}
