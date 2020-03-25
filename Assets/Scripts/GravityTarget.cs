using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityTarget : MonoBehaviour
{
    public float gravityRate = 9.8f;
    public Rigidbody rigidBody { get; private set; }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        if(Gravitation.targets == null)
            Gravitation.targets = new List<GravityTarget>();
        
        Gravitation.targets.Add(this);
    }

    private void OnDisable()
    {
        Gravitation.targets.Remove(this);
    }
}
