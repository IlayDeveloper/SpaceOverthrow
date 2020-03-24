using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityTarget : MonoBehaviour
{
    public Rigidbody rigidBody { get; private set; }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        if(Gravitation.targets == null)
            Gravitation.targets = new List<Rigidbody>();
        
        Gravitation.targets.Add(rigidBody);
    }

    private void OnDisable()
    {
        Gravitation.targets.Remove(rigidBody);
    }
}
