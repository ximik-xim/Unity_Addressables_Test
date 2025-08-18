using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ExampleAddForceRigidbody : MonoBehaviour
{
    [SerializeField] 
    private KeyCode _keyForward = KeyCode.W;
    
    [SerializeField] 
    private KeyCode _keyBackward = KeyCode.S;
    
    [SerializeField]
    private Rigidbody _rigidbody;

    [SerializeField] 
    private float _force;
    
    private void Update()
    {
        if (Input.GetKey(_keyForward))
        {
            AddForce(_force);
        }
        
        if (Input.GetKey(_keyBackward))
        {
            AddForce(-_force);
        }
    }

    private void AddForce(float force)
    {
        Vector3 dir = force * transform.forward * Time.deltaTime;
        _rigidbody.AddForce(dir);
    }
}
