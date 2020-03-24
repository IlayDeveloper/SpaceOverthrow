using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Gravitation : MonoBehaviour
{
    public const float g = 9.8f;
    private float planetMass;
    //private float shipMass;
    //public Rigidbody rigidbody;
    public static List<Rigidbody> targets { get; set; }
    void Start()
    {
        planetMass = GetComponent<Rigidbody>().mass;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*if (SpaceShip.Instance != null)
        {
            float force = g * (planetMass * shipMass) / Vector3.SqrMagnitude(SpaceShip.Instance.transform.position - transform.position);
            SpaceShip.Instance.rigidbody.AddForce(( transform.position - SpaceShip.Instance.transform.position ).normalized * force);
        }*/

        foreach (var target in targets)
        {
            AddForce(target);
        }
    }

    private void AddForce(Rigidbody target)
    {
        float force = g * (planetMass * target.mass) / Vector3.SqrMagnitude(SpaceShip.Instance.transform.position - transform.position);
        target.AddForce(( transform.position - target.transform.position ).normalized * force);
    }
}
