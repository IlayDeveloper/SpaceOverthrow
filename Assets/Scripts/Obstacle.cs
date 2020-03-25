using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class Obstacle : MonoBehaviour
{
    public GameObject explosion;
    private void OnCollisionEnter(Collision other)
    {
        GameObject exp = Instantiate(explosion);
        exp.transform.position = transform.position;
        Destroy(gameObject);
    }

    private void Awake()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 t = new Vector3(Random.Range(-1000,1000), Random.Range(-1000,1000), Random.Range(-1000,1000));
        rb.AddTorque(t);
    }
}
