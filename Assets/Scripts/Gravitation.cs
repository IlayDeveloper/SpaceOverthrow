using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravitation : MonoBehaviour
{
    public float g = 9.8f;
    private float planetMass;
    private float shipMass;
    public Rigidbody rigidbody;
    //private 
    // Start is called before the first frame update
    void Start()
    {
        planetMass = rigidbody.mass;
        shipMass = SpaceShip.Instance.rigidbody.mass;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (SpaceShip.Instance != null)
        {
            float force = g * (planetMass * shipMass) / Vector3.SqrMagnitude(SpaceShip.Instance.transform.position - transform.position);
            SpaceShip.Instance.rigidbody.AddForce(( transform.position - SpaceShip.Instance.transform.position ).normalized * force);
        }
    }
}
